using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using ClinicApp;
using ClinicApp.Comparators;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

class Program
{
    static ClinicLogger _logger;

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

        Clinic clinic = new Clinic();
        _logger = clinic.Logger;

        PatientManager patientManager = clinic.Patients;
        DoctorManager doctorManager = clinic.Doctors;
        AppointmentManager appointmentManager = clinic.Appointments;

        patientManager.Add(new Patient("Ivan", "Petrenko", new DateTime(1992, 3, 14), "0501234567", "Flu", BloodType.APositive));
        patientManager.Add(new Patient("Olena", "Kovalchuk", new DateTime(1981, 7, 2), "0671112233"));
        patientManager.Add(new Patient("Mykola", "Sydorenko", new DateTime(1956, 11, 23), "0939998877", "Hypertension", BloodType.ONegative));

        doctorManager.Add(new Doctor("Petro", "Ivanenko", "AA-1001", "0504445566", Speciality.General, new WorkSchedule(8, 16)));
        doctorManager.Add(new Doctor("Maria", "Shevchenko", "AA-1002", "0507778899", Speciality.Surgery, new WorkSchedule(10, 18)));
        doctorManager.Add(new Doctor("Oleg", "Tkachenko", "AA-1003", "0631234321", Speciality.Cardiology, new WorkSchedule(12, 20)));

        appointmentManager.Book(1, 1, DateTime.Now.AddDays(1));
        appointmentManager.Book(2, 2, DateTime.Now.AddDays(2), 45);
        appointmentManager.BookUrgent(3, 3, DateTime.Today.AddDays(3).AddHours(13), "біль у грудях");
        appointmentManager.BookSpecialist(1, 3, DateTime.Today.AddDays(4).AddHours(14), "кардіологія", 60);

        MedicalRecordManager recordManager = clinic.MedicalRecords;

        recordManager.Add(new Diagnosis(1, 1, DateTime.Today.AddDays(-30), "I10", "Гіпертонічна хвороба", true));
        recordManager.Add(new Diagnosis(1, 1, DateTime.Today.AddDays(-5), "J06.9", "Гострий ринофарингіт"));
        recordManager.Add(new LabResult(1, 1, DateTime.Today.AddDays(-20), "Гемоглобін", 145, "г/л", "120–160", true));
        recordManager.Add(new LabResult(1, 1, DateTime.Today.AddDays(-3), "Холестерин", 6.2, "ммоль/л", "< 5.2", false));
        recordManager.Add(new Prescription(1, 1, DateTime.Today.AddDays(-5), "Лізиноприл", "10 мг", 30, "1 раз на добу вранці"));
        recordManager.Add(new Prescription(2, 2, DateTime.Today.AddDays(-40), "Амоксицилін", "500 мг", 10, "після їжі"));
        recordManager.Add(new Diagnosis(2, 2, DateTime.Today.AddDays(-15), "K29.7", "Гастрит"));
        recordManager.Add(new LabResult(3, 3, DateTime.Today.AddDays(-2), "Глюкоза", 5.1, "ммоль/л", "3.9–5.5", true));

        Console.WriteLine("=== Demo: overloads ===");
        Doctor byEnum = doctorManager.FindBySpeciality(Speciality.Cardiology);
        Doctor byText = doctorManager.FindBySpeciality("кардіо");
        Console.WriteLine(byEnum);
        Console.WriteLine(byText);

        DateTime tomorrow = DateTime.Now.AddDays(1);
        Appointment[] tomorrowList = appointmentManager.GetByDate(tomorrow.Year, tomorrow.Month, tomorrow.Day);
        appointmentManager.DisplayList(tomorrowList);

        Console.WriteLine("=== Demo: TryFindById ===");
        if (patientManager.TryFindById(1, out Patient foundPatient))
            Console.WriteLine("Found: " + foundPatient.FullName);
        else
            Console.WriteLine("Patient not found.");

        Console.WriteLine(patientManager.TryFindById(99, out Patient missingPatient));

        if (doctorManager.TryFindById(2, out Doctor foundDoctor))
            Console.WriteLine("Found: " + foundDoctor.FullName);
        else
            Console.WriteLine("Doctor not found.");

        Console.WriteLine("=== Demo: ?. and ?? ===");
        string name = patientManager.FindById(99)?.FullName ?? "not found";
        Console.WriteLine(name);
        string name2 = patientManager.FindById(1)?.FullName ?? "not found";
        Console.WriteLine(name2);

        Console.WriteLine("=== Demo: FindByBloodType ===");
        Patient[] withBlood = patientManager.FindByBloodType(BloodType.APositive);
        Console.WriteLine(withBlood.Length);

        Console.WriteLine("=== Demo: indexers ===");
        Console.WriteLine(patientManager[0]);
        Console.WriteLine(doctorManager[1]);
        Console.WriteLine(patientManager[999] == null);

        Console.WriteLine("=== Demo: validation ===");
        try
        {
            Patient bad = new Patient("", "Petrenko", new DateTime(1990, 1, 1), "0501234567");
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }

        try
        {
            Patient bad2 = new Patient("Test", "Test", DateTime.Today.AddDays(1), "0501234567");
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }

        try
        {
            WorkSchedule badSchedule = new WorkSchedule(20, 6);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }

        Console.WriteLine("== new vs override ==");

        UrgentAppointment urgent = new UrgentAppointment(1, 1, DateTime.Today.AddDays(5).AddHours(10), "тест");
        Appointment viaBase = urgent;

        Console.WriteLine("Через Appointment:       " + viaBase.GetDescription() + " | пріоритет " + viaBase.GetPriority());
        Console.WriteLine("Через UrgentAppointment: " + urgent.GetDescription() + " | пріоритет " + urgent.GetPriority());

        // 1. Чому GetDescription() через змінну типу Appointment дає опис термінового прийому?
        //    GetDescription() в Appointment оголошений як virtual, а в UrgentAppointment
        //    перевизначений через override. Для таких методів реалізацію обирає CLR
        //    під час виконання за типом ОБ'ЄКТА (UrgentAppointment), а не за типом змінної.
        //
        // 2. Чому GetPriority() через ту саму змінну повертає 3, хоча об'єкт — UrgentAppointment?
        //    GetPriority() в Appointment не virtual, а в UrgentAppointment він лише прихований
        //    через new. Приховування — не перевизначення: метод обирається ще під час компіляції
        //    за типом ЗМІННОЇ. Змінна типу Appointment бачить метод батька, тому результат 3.
        //
        // 3. Що змінити, щоб GetPriority() теж працював поліморфно?
        //    В Appointment: public virtual int GetPriority().
        //    В UrgentAppointment: public override int GetPriority() замість public new int GetPriority().
        //    Тоді обидві змінні повернуть 1.

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("╔" + new string('═', 46) + "╗");
            MenuLine("           МЕДИЧНА КЛІНІКА");
            Console.WriteLine("╠" + new string('═', 46) + "╣");
            MenuLine("  1. Пацієнти       — реєстрація, пошук");
            MenuLine("  2. Лікарі         — персонал, розклад");
            MenuLine("  3. Записи         — прийоми, скасування");
            MenuLine("  4. Медична картка — діагнози, рецепти");
            MenuLine("  5. Рахунки        — оплата, борги");
            MenuLine("  6. Черга          — очікування, прийом");
            MenuLine("  7. Звіт           — загальна статистика");
            MenuLine("  8. Аналітика      — лікарі, пацієнти");
            MenuLine("  9. Плани лікування — рефлексія, атрибути");
            MenuLine("  10. Файли         — лог, експорт");
            MenuLine("  0. Вийти");
            Console.WriteLine("╚" + new string('═', 46) + "╝");
            Console.Write("Оберіть розділ: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PatientsMenu(patientManager);
                    break;

                case "2":
                    DoctorsMenu(doctorManager);
                    break;

                case "3":
                    AppointmentsMenu(appointmentManager, patientManager, doctorManager);
                    break;

                case "4":
                    MedicalRecordsMenu(recordManager, patientManager, doctorManager);
                    break;

                case "5":
                    BillingMenu(clinic.Billing);
                    break;

                case "6":
                    WaitingRoomMenu(clinic);
                    break;

                case "7":
                    clinic.Report();
                    break;

                case "8":
                    AnalyticsMenu(clinic);
                    break;

                case "9":
                    TreatmentPlansMenu(clinic.TreatmentPlans);
                    break;

                case "10":
                    FilesMenu(clinic);
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }

    static void PatientsMenu(PatientManager manager)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.WriteLine();
            Console.WriteLine("=== Patients menu ===");
            Console.WriteLine("1. Show all");
            Console.WriteLine("2. Add");
            Console.WriteLine("3. Find by name");
            Console.WriteLine("4. Remove");
            Console.WriteLine("5. Statistics");
            Console.WriteLine("6. Find by blood type");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.ShowAll();
                    break;

                case "2":
                    try
                    {
                        Console.Write("First name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Date of birth (dd.MM.yyyy): ");
                        DateTime dob = DateTime.Parse(Console.ReadLine());
                        Console.Write("Phone (10 digits): ");
                        string phone = Console.ReadLine();
                        Console.Write("Diagnosis: ");
                        string diag = Console.ReadLine();

                        Console.WriteLine("Blood type:");
                        for (int i = 0; i < 9; i++)
                            Console.WriteLine(i + ". " + ClinicFormatter.FormatBloodType((BloodType)i));
                        Console.Write("Choose: ");
                        int bloodNum = int.Parse(Console.ReadLine());

                        if (!Enum.IsDefined(typeof(BloodType), bloodNum))
                            throw new ArgumentOutOfRangeException("BloodType", "Невідома група крові.");

                        BloodType blood = (BloodType)bloodNum;

                        Patient newPatient = new Patient(firstName, lastName, dob, phone, diag, blood);
                        manager.Add(newPatient);
                        Console.WriteLine("Added.");
                        _logger.LogInfo("Додано пацієнта #" + newPatient.Id + ": " + newPatient.FullName);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                        _logger.LogWarning("Помилка введення: " + e.Message);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                        _logger.LogWarning("Помилка введення: " + e.Message);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                        _logger.LogWarning("Помилка введення: неправильний формат введення.");
                    }
                    break;

                case "3":
                    Console.Write("Name to find: ");
                    string search = Console.ReadLine();
                    Patient found = manager.FindByName(search);

                    if (found != null)
                        Console.WriteLine(found);
                    else
                        Console.WriteLine("Not found.");
                    break;

                case "4":
                    try
                    {
                        Console.Write("Id to remove: ");
                        int id = int.Parse(Console.ReadLine());

                        if (manager.Remove(id))
                            Console.WriteLine("Removed.");
                        else
                            Console.WriteLine("Not found.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                    }
                    break;

                case "5":
                    manager.Statistics();
                    break;

                case "6":
                    try
                    {
                        Console.WriteLine("Blood type:");
                        for (int i = 0; i < 9; i++)
                            Console.WriteLine(i + ". " + ClinicFormatter.FormatBloodType((BloodType)i));
                        Console.Write("Choose: ");
                        int btNum = int.Parse(Console.ReadLine());

                        if (!Enum.IsDefined(typeof(BloodType), btNum))
                        {
                            Console.WriteLine("Помилка: невідома група крові.");
                            break;
                        }

                        Patient[] list = manager.FindByBloodType((BloodType)btNum);

                        if (list.Length == 0)
                            Console.WriteLine("Not found.");
                        else
                            foreach (var p in list)
                                Console.WriteLine(p);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                    }
                    break;

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }

    static void DoctorsMenu(DoctorManager manager)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.WriteLine();
            Console.WriteLine("=== Doctors menu ===");
            Console.WriteLine("1. Show all");
            Console.WriteLine("2. Add");
            Console.WriteLine("3. Find by speciality");
            Console.WriteLine("4. Remove");
            Console.WriteLine("5. Вільні години лікаря");
            Console.WriteLine("6. Statistics");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.ShowAll();
                    break;

                case "2":
                    try
                    {
                        Console.Write("First name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("License number: ");
                        string license = Console.ReadLine();
                        Console.Write("Phone (10 digits): ");
                        string phone = Console.ReadLine();

                        Console.WriteLine("Speciality:");
                        for (int i = 0; i < 8; i++)
                            Console.WriteLine(i + ". " + ClinicFormatter.FormatSpeciality((Speciality)i));
                        Console.Write("Choose: ");
                        int specNum = int.Parse(Console.ReadLine());

                        if (!Enum.IsDefined(typeof(Speciality), specNum))
                            throw new ArgumentOutOfRangeException("Speciality", "Невідома спеціальність.");

                        Speciality spec = (Speciality)specNum;

                        Console.Write("Start hour: ");
                        int start = int.Parse(Console.ReadLine());
                        Console.Write("End hour: ");
                        int end = int.Parse(Console.ReadLine());

                        WorkSchedule schedule = new WorkSchedule(start, end);

                        Doctor newDoctor = new Doctor(firstName, lastName, license, phone, spec, schedule);
                        manager.Add(newDoctor);
                        Console.WriteLine("Added.");
                        _logger.LogInfo("Додано лікаря #" + newDoctor.Id + ": " + newDoctor.FullName);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                        _logger.LogWarning("Помилка введення: " + e.Message);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                        _logger.LogWarning("Помилка введення: " + e.Message);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                        _logger.LogWarning("Помилка введення: неправильний формат введення.");
                    }
                    break;

                case "3":
                    Console.Write("Speciality to find: ");
                    string search = Console.ReadLine();
                    Doctor found = manager.FindBySpeciality(search);

                    if (found != null)
                        Console.WriteLine(found);
                    else
                        Console.WriteLine("Not found.");
                    break;

                case "4":
                    try
                    {
                        Console.Write("Id to remove: ");
                        int id = int.Parse(Console.ReadLine());

                        if (manager.Remove(id))
                            Console.WriteLine("Removed.");
                        else
                            Console.WriteLine("Not found.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                    }
                    break;

                case "5":
                    {
                        Console.Write("ID лікаря: ");
                        if (!int.TryParse(Console.ReadLine(), out int did))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        Doctor doc = manager.FindById(did);

                        if (doc == null)
                        {
                            Console.WriteLine("Лікаря не знайдено");
                            break;
                        }

                        ISchedulable sch = doc;

                        Console.Write("Дата (dd.MM.yyyy): ");
                        if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime day))
                        {
                            Console.WriteLine("Помилка: некоректна дата.");
                            break;
                        }

                        Console.Write("Скільки слотів: ");
                        if (!int.TryParse(Console.ReadLine(), out int n))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        try
                        {
                            DateTime[] slots = sch.GetAvailableSlots(day, n);

                            for (int i = 0; i < slots.Length; i++)
                                Console.WriteLine(slots[i].ToString("HH:mm"));
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        break;
                    }

                case "6":
                    manager.Statistics();
                    break;

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }

    static void WaitingRoomMenu(Clinic clinic)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.WriteLine();
            Console.WriteLine("── Черга ───────────────────────");
            Console.WriteLine("  1. Додати пацієнта до черги");
            Console.WriteLine("  2. Прийняти першого");
            Console.WriteLine("  3. Хто перший?");
            Console.WriteLine("  4. Переглянути всю чергу");
            Console.WriteLine("  0. Назад");
            Console.Write("Оберіть: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        Console.Write("ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        Patient p = clinic.Patients.FindById(pid);

                        if (p == null)
                        {
                            Console.WriteLine("Пацієнта не знайдено.");
                            break;
                        }

                        clinic.WaitingRoom.Enqueue(p);
                        Console.WriteLine("Додано до черги: " + p.FullName + ". У черзі: " + clinic.WaitingRoom.Count);
                        break;
                    }

                case "2":
                    try
                    {
                        Patient next = clinic.WaitingRoom.Dequeue();
                        Console.WriteLine("Прийнято: " + next.FullName + ". У черзі лишилось: " + clinic.WaitingRoom.Count);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    break;

                case "3":
                    try
                    {
                        Patient first = clinic.WaitingRoom.Peek();
                        Console.WriteLine("Перший у черзі: " + first.FullName);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    break;

                case "4":
                    {
                        Patient[] all = clinic.WaitingRoom.ToArray();

                        if (all.Length == 0)
                        {
                            Console.WriteLine("Черга порожня.");
                            break;
                        }

                        for (int i = 0; i < all.Length; i++)
                            Console.WriteLine((i + 1) + ". " + all[i].FullName);
                        break;
                    }

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }

    static List<DoctorStats> CollectDoctorStats(Clinic clinic)
    {
        List<DoctorStats> list = new List<DoctorStats>();

        foreach (DoctorStats s in clinic.Analytics.ComputeDoctorStats())
            list.Add(s);

        return list;
    }

    static List<PatientStats> CollectPatientStats(Clinic clinic)
    {
        List<PatientStats> list = new List<PatientStats>();

        foreach (PatientStats s in clinic.Analytics.ComputePatientStats())
            list.Add(s);

        return list;
    }

    static void AnalyticsMenu(Clinic clinic)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.WriteLine();
            Console.WriteLine("── Аналітика ───────────────────");
            Console.WriteLine("  1. Лікарі за навантаженням");
            Console.WriteLine("  2. Лікарі за виручкою");
            Console.WriteLine("  3. Лікарі за іменем");
            Console.WriteLine("  4. Пацієнти за кількістю візитів");
            Console.WriteLine("  5. Пацієнти за витратами");
            Console.WriteLine("  0. Назад");
            Console.Write("Оберіть: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        List<DoctorStats> list = CollectDoctorStats(clinic);
                        list.Sort();

                        foreach (DoctorStats s in list)
                            Console.WriteLine(s);
                        break;
                    }

                case "2":
                    {
                        List<DoctorStats> list = CollectDoctorStats(clinic);
                        list.Sort(new DoctorStatsByRevenue());

                        foreach (DoctorStats s in list)
                            Console.WriteLine(s);
                        break;
                    }

                case "3":
                    {
                        List<DoctorStats> list = CollectDoctorStats(clinic);
                        list.Sort(new DoctorStatsByName());

                        foreach (DoctorStats s in list)
                            Console.WriteLine(s);
                        break;
                    }

                case "4":
                    {
                        List<PatientStats> list = CollectPatientStats(clinic);
                        list.Sort();

                        foreach (PatientStats s in list)
                            Console.WriteLine(s);
                        break;
                    }

                case "5":
                    {
                        List<PatientStats> list = CollectPatientStats(clinic);
                        list.Sort(new PatientStatsBySpent());

                        foreach (PatientStats s in list)
                            Console.WriteLine(s);
                        break;
                    }

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }

    static void TreatmentPlansMenu(TreatmentPlanManager manager)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.WriteLine();
            Console.WriteLine("── Плани лікування ────────────────────");
            Console.WriteLine("  1. Показати всі плани");
            Console.WriteLine("  2. Додати план лікування");
            Console.WriteLine("  3. Плани пацієнта");
            Console.WriteLine("  4. Активувати план");
            Console.WriteLine("  5. Завершити план");
            Console.WriteLine("  6. Скасувати план");
            Console.WriteLine("  7. Інформація про тип TreatmentPlan");
            Console.WriteLine("  0. Назад");
            Console.Write("Оберіть: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        TreatmentPlan[] all = manager.GetAll();

                        if (all.Length == 0)
                        {
                            Console.WriteLine("Планів ще немає.");
                            break;
                        }

                        for (int i = 0; i < all.Length; i++)
                            Console.WriteLine(all[i]);
                        break;
                    }

                case "2":
                    {
                        TreatmentPlan plan = FormBuilder.Build<TreatmentPlan>();

                        if (manager.Add(plan))
                            Console.WriteLine("План [" + plan.Id + "] додано.");
                        break;
                    }

                case "3":
                    {
                        Console.Write("ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        TreatmentPlan[] list = manager.GetByPatient(pid);

                        if (list.Length == 0)
                        {
                            Console.WriteLine("Not found.");
                            break;
                        }

                        for (int i = 0; i < list.Length; i++)
                            Console.WriteLine(list[i]);
                        break;
                    }

                case "4":
                    {
                        Console.Write("ID плану: ");
                        if (!int.TryParse(Console.ReadLine(), out int aid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        TreatmentPlan plan = manager.GetById(aid);

                        if (plan == null)
                        {
                            Console.WriteLine("План не знайдено.");
                            break;
                        }

                        if (plan.Activate())
                            Console.WriteLine("План [" + aid + "] активовано.");
                        else
                            Console.WriteLine("Не вдалося активувати: поточний статус " + plan.Status + ".");
                        break;
                    }

                case "5":
                    {
                        Console.Write("ID плану: ");
                        if (!int.TryParse(Console.ReadLine(), out int cid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        TreatmentPlan plan = manager.GetById(cid);

                        if (plan == null)
                        {
                            Console.WriteLine("План не знайдено.");
                            break;
                        }

                        if (plan.Complete())
                            Console.WriteLine("План [" + cid + "] завершено.");
                        else
                            Console.WriteLine("Не вдалося завершити: поточний статус " + plan.Status + ".");
                        break;
                    }

                case "6":
                    {
                        Console.Write("ID плану: ");
                        if (!int.TryParse(Console.ReadLine(), out int xid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        TreatmentPlan plan = manager.GetById(xid);

                        if (plan == null)
                        {
                            Console.WriteLine("План не знайдено.");
                            break;
                        }

                        if (plan.Cancel())
                            Console.WriteLine("План [" + xid + "] скасовано.");
                        else
                            Console.WriteLine("Не вдалося скасувати: поточний статус " + plan.Status + ".");
                        break;
                    }

                case "7":
                    ModelValidator.PrintInfo(typeof(TreatmentPlan));
                    break;

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }

    static void FilesMenu(Clinic clinic)
    {
        ClinicLogger logger = clinic.Logger;
        bool inMenu = true;

        while (inMenu)
        {
            Console.WriteLine();
            Console.WriteLine("── Файли ───────────────────────");
            Console.WriteLine("  1. Останні рядки лога");
            Console.WriteLine("  2. Очистити лог");
            Console.WriteLine("  3. Експортувати всі звіти");
            Console.WriteLine("  4. Імпорт пацієнтів з CSV");
            Console.WriteLine("  0. Назад");
            Console.Write("Оберіть: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        Console.Write("Скільки рядків: ");
                        if (!int.TryParse(Console.ReadLine(), out int n))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        string[] lines = logger.GetLastLines(n);

                        if (lines.Length == 0)
                        {
                            Console.WriteLine("Лог порожній або файлу немає.");
                            break;
                        }

                        for (int i = 0; i < lines.Length; i++)
                            Console.WriteLine(lines[i]);
                        break;
                    }

                case "2":
                    logger.Clear();
                    Console.WriteLine("Лог очищено.");
                    break;

                case "3":
                    try
                    {
                        string[] paths = clinic.Exporter.ExportAll();

                        Console.WriteLine("Створено файли:");
                        for (int i = 0; i < paths.Length; i++)
                            Console.WriteLine("  " + paths[i]);

                        logger.LogInfo("Експортовано звіти: " + paths.Length + " файли.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Помилка експорту: " + e.Message);
                        logger.LogError("Помилка експорту: " + e.Message);
                    }
                    break;

                case "4":
                    {
                        Console.Write("Шлях до CSV-файлу: ");
                        string csvPath = Console.ReadLine().Trim().Trim('"');

                        ImportResult importResult = CsvImporter.ImportPatients(clinic, csvPath);
                        importResult.Print();

                        logger.LogInfo("Імпорт пацієнтів з CSV: імпортовано " + importResult.Imported + ", пропущено " + importResult.Skipped);
                        break;
                    }

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }

    static void MenuLine(string text)
    {
        Console.WriteLine("║" + text.PadRight(46) + "║");
    }

    static void BillingMenu(BillingManager billing)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.WriteLine();
            Console.WriteLine("── Рахунки ───────────────────");
            Console.WriteLine("  1. Борги пацієнта");
            Console.WriteLine("  2. Всі неоплачені записи");
            Console.WriteLine("  3. Оплатити запис");
            Console.WriteLine("  4. Загальний борг клініки");
            Console.WriteLine("  0. Назад");
            Console.Write("Оберіть: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        Console.Write("ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        IPayable[] unpaid = billing.GetUnpaidByPatient(pid);

                        Console.WriteLine("Неоплачені записи пацієнта #" + pid + ":");
                        billing.DisplayUnpaid(unpaid);
                        Console.WriteLine("Борг: " + billing.GetPatientDebt(pid).ToString("F2") + " грн");
                        break;
                    }

                case "2":
                    {
                        IPayable[] unpaid = billing.GetAllUnpaid();
                        billing.DisplayUnpaid(unpaid);
                        break;
                    }

                case "3":
                    {
                        Console.Write("ID запису для оплати: ");
                        if (!int.TryParse(Console.ReadLine(), out int aid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        if (billing.PayAppointment(aid))
                        {
                            Console.WriteLine("Запис [" + aid + "] оплачено.");
                            _logger.LogInfo("Запис #" + aid + " оплачено.");
                        }
                        else
                            Console.WriteLine("Не вдалося оплатити: запис не знайдено, вже оплачено або скасовано.");
                        break;
                    }

                case "4":
                    Console.WriteLine("Загальний борг по клініці: " + billing.GetTotalDebt().ToString("F2") + " грн");
                    break;

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }

    static bool ReadPatientAndDoctor(PatientManager patients, DoctorManager doctors, out int pid, out int did)
    {
        pid = 0;
        did = 0;

        Console.WriteLine("Patients:");
        patients.ShowAll();
        Console.WriteLine("Doctors:");
        doctors.ShowAll();

        Console.Write("ID пацієнта: ");
        if (!int.TryParse(Console.ReadLine(), out pid))
        {
            Console.WriteLine("Помилка: некоректне число.");
            return false;
        }

        Console.Write("ID лікаря: ");
        if (!int.TryParse(Console.ReadLine(), out did))
        {
            Console.WriteLine("Помилка: некоректне число.");
            return false;
        }

        if (patients.FindById(pid) == null)
        {
            Console.WriteLine("Пацієнта з ID " + pid + " не знайдено.");
            return false;
        }

        if (doctors.FindById(did) == null)
        {
            Console.WriteLine("Лікаря з ID " + did + " не знайдено.");
            return false;
        }

        return true;
    }

    static void MedicalRecordsMenu(MedicalRecordManager manager, PatientManager patients, DoctorManager doctors)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.WriteLine();
            Console.WriteLine("── Медична картка ────────────");
            Console.WriteLine("1. Картка пацієнта");
            Console.WriteLine("2. Усі записи пацієнта");
            Console.WriteLine("3. Додати діагноз");
            Console.WriteLine("4. Додати аналіз");
            Console.WriteLine("5. Додати рецепт");
            Console.WriteLine("6. Записи лікаря");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        Console.Write("ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        if (patients.FindById(pid) == null)
                        {
                            Console.WriteLine("Пацієнта з ID " + pid + " не знайдено.");
                            break;
                        }

                        manager.DisplayPatientSummary(pid);
                        break;
                    }

                case "2":
                    {
                        Console.Write("ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int pid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        if (patients.FindById(pid) == null)
                        {
                            Console.WriteLine("Пацієнта з ID " + pid + " не знайдено.");
                            break;
                        }

                        MedicalRecord[] records = manager.GetByPatient(pid);

                        if (records.Length == 0)
                        {
                            Console.WriteLine("Not found.");
                            break;
                        }

                        for (int i = 0; i < records.Length; i++)
                            Console.WriteLine(records[i].GetRecordType() + ": " + records[i].GetSummary());
                        break;
                    }

                case "3":
                    {
                        if (!ReadPatientAndDoctor(patients, doctors, out int pid, out int did))
                            break;

                        Console.Write("Код діагнозу (напр. J06.9): ");
                        string code = Console.ReadLine();
                        Console.Write("Опис: ");
                        string desc = Console.ReadLine();
                        Console.Write("Хронічне? (1=так, 0=ні): ");
                        string chr = Console.ReadLine();

                        if (chr != "1" && chr != "0")
                        {
                            Console.WriteLine("Помилка: введіть 1 або 0.");
                            break;
                        }

                        try
                        {
                            manager.Add(new Diagnosis(pid, did, DateTime.Today, code, desc, chr == "1"));
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        break;
                    }

                case "4":
                    {
                        if (!ReadPatientAndDoctor(patients, doctors, out int pid, out int did))
                            break;

                        Console.Write("Назва аналізу: ");
                        string testName = Console.ReadLine();
                        Console.Write("Значення (число): ");
                        if (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double val))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        Console.Write("Одиниці виміру: ");
                        string unit = Console.ReadLine();
                        Console.Write("Норма (напр. 4.0–9.0): ");
                        string range = Console.ReadLine();
                        Console.Write("В нормі? (1=так, 0=ні): ");
                        string norm = Console.ReadLine();

                        if (norm != "1" && norm != "0")
                        {
                            Console.WriteLine("Помилка: введіть 1 або 0.");
                            break;
                        }

                        try
                        {
                            manager.Add(new LabResult(pid, did, DateTime.Today, testName, val, unit, range, norm == "1"));
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        break;
                    }

                case "5":
                    {
                        if (!ReadPatientAndDoctor(patients, doctors, out int pid, out int did))
                            break;

                        Console.Write("Препарат: ");
                        string med = Console.ReadLine();
                        Console.Write("Дозування (10 мг): ");
                        string dose = Console.ReadLine();
                        Console.Write("Кількість днів: ");
                        if (!int.TryParse(Console.ReadLine(), out int days))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        Console.Write("Інструкція (Enter — пропустити): ");
                        string instr = Console.ReadLine();

                        try
                        {
                            manager.Add(new Prescription(pid, did, DateTime.Today, med, dose, days, instr));
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Помилка: " + e.Message);
                        }
                        break;
                    }

                case "6":
                    {
                        Console.Write("ID лікаря: ");
                        if (!int.TryParse(Console.ReadLine(), out int did))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        if (doctors.FindById(did) == null)
                        {
                            Console.WriteLine("Лікаря з ID " + did + " не знайдено.");
                            break;
                        }

                        manager.DisplayList(manager.GetByDoctor(did));
                        break;
                    }

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }

    static void AppointmentsMenu(AppointmentManager manager, PatientManager patients, DoctorManager doctors)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.WriteLine();
            Console.WriteLine("=== Appointments menu ===");
            Console.WriteLine("1. Book");
            Console.WriteLine("2. Cancel");
            Console.WriteLine("3. Complete");
            Console.WriteLine("4. By patient");
            Console.WriteLine("5. By doctor");
            Console.WriteLine("6. By date");
            Console.WriteLine("7. Upcoming");
            Console.WriteLine("8. Скасувати всі записи пацієнта");
            Console.WriteLine("9. За типом прийому");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    try
                    {
                        Console.WriteLine("Patients:");
                        patients.ShowAll();
                        Console.WriteLine("Doctors:");
                        doctors.ShowAll();

                        Console.Write("Patient id: ");
                        int pid = int.Parse(Console.ReadLine());
                        Console.Write("Doctor id: ");
                        int did = int.Parse(Console.ReadLine());
                        Console.Write("Date and time (dd.MM.yyyy HH:mm): ");
                        DateTime dt = DateTime.Parse(Console.ReadLine());
                        Console.Write("Duration minutes (empty = 30): ");
                        string durStr = Console.ReadLine();
                        int dur = durStr.Length > 0 ? int.Parse(durStr) : 30;

                        if (manager.Book(pid, did, dt, dur))
                        {
                            Appointment last = manager[manager.Count - 1];
                            _logger.LogInfo("Запис #" + last.Id + " створено: пацієнт #" + pid + " → лікар #" + did);
                        }
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                        _logger.LogWarning("Помилка введення: " + e.Message);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                        _logger.LogWarning("Помилка введення: " + e.Message);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                        _logger.LogWarning("Помилка введення: неправильний формат введення.");
                    }
                    break;

                case "2":
                    try
                    {
                        Console.Write("Appointment id: ");
                        int cancelId = int.Parse(Console.ReadLine());
                        Console.Write("Reason (empty = none): ");
                        string reason = Console.ReadLine();

                        if (manager.Cancel(cancelId, reason))
                        {
                            Console.WriteLine("Appointment [" + cancelId + "] cancelled.");
                            _logger.LogInfo("Запис #" + cancelId + " скасовано. Причина: " + reason);
                        }
                        else
                            Console.WriteLine("Not found or already final.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                    }
                    break;

                case "3":
                    try
                    {
                        Console.Write("Appointment id: ");
                        int compId = int.Parse(Console.ReadLine());

                        if (manager.Complete(compId))
                        {
                            Console.WriteLine("Appointment [" + compId + "] completed.");
                            _logger.LogInfo("Запис #" + compId + " завершено.");
                        }
                        else
                            Console.WriteLine("Not found or already final.");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                    }
                    break;

                case "4":
                    try
                    {
                        Console.Write("Patient id: ");
                        int byPatientId = int.Parse(Console.ReadLine());
                        manager.DisplayList(manager.GetByPatient(byPatientId));
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                    }
                    break;

                case "5":
                    try
                    {
                        Console.Write("Doctor id: ");
                        int byDoctorId = int.Parse(Console.ReadLine());
                        manager.DisplayList(manager.GetByDoctor(byDoctorId));
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                    }
                    break;

                case "6":
                    try
                    {
                        Console.Write("Date (dd.MM.yyyy): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());
                        manager.DisplayList(manager.GetByDate(date));
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
                    }
                    break;

                case "7":
                    Console.WriteLine("Upcoming appointments:");
                    manager.DisplayList(manager.GetUpcoming());
                    break;

                case "8":
                    {
                        Console.Write("ID пацієнта: ");
                        if (!int.TryParse(Console.ReadLine(), out int cid))
                        {
                            Console.WriteLine("Помилка: некоректне число.");
                            break;
                        }

                        Console.Write("Причина (Enter — без причини): ");
                        string why = Console.ReadLine() ?? "";

                        int cancelled = AppointmentManager.CancelAll(manager.GetByPatient(cid), why);
                        Console.WriteLine("Скасовано записів: " + cancelled);
                        break;
                    }

                case "9":
                    {
                        Console.WriteLine("  1. Термінові");
                        Console.WriteLine("  2. Консультації спеціаліста");
                        Console.WriteLine("  3. Звичайні");
                        Console.Write("Оберіть: ");
                        string typeChoice = Console.ReadLine();

                        switch (typeChoice)
                        {
                            case "1":
                                manager.DisplayList(manager.GetUrgent());
                                break;

                            case "2":
                                manager.DisplayList(manager.GetSpecialist());
                                break;

                            case "3":
                                manager.DisplayList(manager.GetRegular());
                                break;

                            default:
                                Console.WriteLine("Wrong option.");
                                break;
                        }
                        break;
                    }

                case "0":
                    inMenu = false;
                    break;

                default:
                    Console.WriteLine("Wrong option.");
                    break;
            }
        }
    }
}