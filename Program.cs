using System;
using ClinicApp.Enums;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        PatientManager patientManager = new PatientManager();
        DoctorManager doctorManager = new DoctorManager();
        AppointmentManager appointmentManager = new AppointmentManager(patientManager, doctorManager);

        patientManager.Add(new Patient("Ivan", "Petrenko", new DateTime(1992, 3, 14), "0501234567", "Flu", BloodType.APositive));
        patientManager.Add(new Patient("Olena", "Kovalchuk", new DateTime(1981, 7, 2), "0671112233"));
        patientManager.Add(new Patient("Mykola", "Sydorenko", new DateTime(1956, 11, 23), "0939998877", "Hypertension", BloodType.ONegative));

        doctorManager.Add(new Doctor("Petro", "Ivanenko", "AA-1001", "0504445566", Speciality.General, new WorkSchedule(8, 16)));
        doctorManager.Add(new Doctor("Maria", "Shevchenko", "AA-1002", "0507778899", Speciality.Surgery, new WorkSchedule(10, 18)));
        doctorManager.Add(new Doctor("Oleg", "Tkachenko", "AA-1003", "0631234321", Speciality.Cardiology, new WorkSchedule(12, 20)));

        appointmentManager.Book(1, 1, DateTime.Now.AddDays(1));
        appointmentManager.Book(2, 2, DateTime.Now.AddDays(2), 45);

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

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== Main menu ===");
            Console.WriteLine("1. Patients");
            Console.WriteLine("2. Doctors");
            Console.WriteLine("3. Appointments");
            Console.WriteLine("0. Exit");
            Console.Write("Choose: ");

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

                        manager.Add(new Patient(firstName, lastName, dob, phone, diag, blood));
                        Console.WriteLine("Added.");
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
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
            Console.WriteLine("5. Statistics");
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

                        manager.Add(new Doctor(firstName, lastName, license, phone, spec, schedule));
                        Console.WriteLine("Added.");
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
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

                        manager.Book(pid, did, dt, dur);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("Помилка: " + e.Message);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Помилка: неправильний формат введення.");
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
                            Console.WriteLine("Appointment [" + cancelId + "] cancelled.");
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
                            Console.WriteLine("Appointment [" + compId + "] completed.");
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