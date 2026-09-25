using ClinicApp;

Clinic clinic = new Clinic("Медична Клініка");

clinic.Patients.Add(new Patient("Іван", "Петренко", new DateTime(1985, 3, 12), "A+", "0501234567"));
clinic.Patients.Add(new Patient("Олена", "Коваль", new DateTime(1993, 7, 4), "B-", "0672345678"));
clinic.Patients.Add(new Patient("Максим", "Бойко", new DateTime(2010, 1, 20), "O+", "0933456789"));
clinic.Patients.Add(new Patient("Марія", "Ткач"));

Doctor d1 = new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567");
d1.WorkStartHour = 8;
d1.WorkEndHour = 16;
clinic.Doctors.Add(d1);

Doctor d2 = new Doctor("Наталія", "Мороз", "Неврологія", "LIC-002", "0442345678");
d2.WorkStartHour = 9;
d2.WorkEndHour = 18;
clinic.Doctors.Add(d2);

Doctor d3 = new Doctor("Андрій", "Власенко", "Педіатрія");
clinic.Doctors.Add(d3);

clinic.Appointments.Book(1, 1, new DateTime(2026, 5, 9, 10, 0, 0));
clinic.Appointments.Book(2, 2, new DateTime(2026, 5, 9, 11, 0, 0), 45);
clinic.Appointments.Book(3, 3, new DateTime(2026, 5, 10, 9, 0, 0), 20);

Console.WriteLine();
clinic.DisplaySchedule(new DateTime(2026, 5, 9));

Console.WriteLine();
clinic.GenerateReport();


PatientsMenu(clinic);
DoctorsMenu(clinic);
AppointmentsMenu(clinic);


void PatientsMenu(Clinic clinic)
{
    bool exit = false;

    while (!exit)
    {
        Console.WriteLine();
        Console.WriteLine("=== Пацієнти ===");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Додати пацієнта");
        Console.WriteLine("3. Знайти за ім'ям");
        Console.WriteLine("4. Видалити пацієнта");
        Console.WriteLine("5. Статистика");
        Console.WriteLine("0. Вихід");
        Console.Write("Вибір: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                clinic.Patients.DisplayAll();
                break;

            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine()!;
                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine()!;
                clinic.Patients.Add(new Patient(firstName, lastName));
                break;

            case "3":
                Console.Write("Ім'я або прізвище для пошуку: ");
                string query = Console.ReadLine()!;
                Patient[] found = clinic.Patients.FindByName(query);
                if (found.Length == 0)
                {
                    Console.WriteLine("Нічого не знайдено.");
                }
                else
                {
                    foreach (var p in found)
                        Console.WriteLine(p);
                }
                break;

            case "4":
                Console.Write("Id пацієнта для видалення: ");
                int removeId = int.Parse(Console.ReadLine()!);
                bool removed = clinic.Patients.Remove(removeId);
                Console.WriteLine(removed ? "Видалено." : "Пацієнта з таким Id не знайдено.");
                break;

            case "5":
                clinic.Patients.DisplayStats();
                break;

            case "0":
                exit = true;
                break;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}

void DoctorsMenu(Clinic clinic)
{
    bool exit = false;

    while (!exit)
    {
        Console.WriteLine();
        Console.WriteLine("=== Лікарі ===");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Додати лікаря");
        Console.WriteLine("3. Знайти за спеціальністю");
        Console.WriteLine("4. Видалити лікаря");
        Console.WriteLine("5. Статистика");
        Console.WriteLine("0. Вихід");
        Console.Write("Вибір: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                clinic.Doctors.DisplayAll();
                break;

            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine()!;
                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine()!;
                Console.Write("Спеціальність: ");
                string speciality = Console.ReadLine()!;
                clinic.Doctors.Add(new Doctor(firstName, lastName, speciality));
                break;

            case "3":
                Console.Write("Спеціальність для пошуку: ");
                string query = Console.ReadLine()!;
                Doctor[] found = clinic.Doctors.FindBySpeciality(query);
                if (found.Length == 0)
                {
                    Console.WriteLine("Нічого не знайдено.");
                }
                else
                {
                    foreach (var d in found)
                        Console.WriteLine(d);
                }
                break;

            case "4":
                Console.Write("Id лікаря для видалення: ");
                int removeId = int.Parse(Console.ReadLine()!);
                bool removed = clinic.Doctors.Remove(removeId);
                Console.WriteLine(removed ? "Видалено." : "Лікаря з таким Id не знайдено.");
                break;

            case "5":
                clinic.Doctors.DisplayStats();
                break;

            case "0":
                exit = true;
                break;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}

void AppointmentsMenu(Clinic clinic)
{
    bool exit = false;

    while (!exit)
    {
        Console.WriteLine();
        Console.WriteLine("=== Записи ===");
        Console.WriteLine("1. Записати");
        Console.WriteLine("2. Скасувати");
        Console.WriteLine("3. Завершити");
        Console.WriteLine("4. Записи пацієнта");
        Console.WriteLine("5. Записи лікаря");
        Console.WriteLine("6. Розклад на дату");
        Console.WriteLine("7. Майбутні записи");
        Console.WriteLine("8. Звіт клініки");
        Console.WriteLine("0. Вихід");
        Console.Write("Вибір: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                clinic.Patients.DisplayAll();
                clinic.Doctors.DisplayAll();

                Console.Write("Id пацієнта: ");
                int patientId = int.Parse(Console.ReadLine()!);
                Console.Write("Id лікаря: ");
                int doctorId = int.Parse(Console.ReadLine()!);
                Console.Write("Дата й час (dd.MM.yyyy HH:mm): ");
                DateTime scheduledAt = DateTime.Parse(Console.ReadLine()!);
                Console.Write("Тривалість у хвилинах (Enter = 30): ");
                string durationInput = Console.ReadLine()!;

                int duration;
                if (!int.TryParse(durationInput, out duration))
                    duration = 30;

                clinic.Appointments.Book(patientId, doctorId, scheduledAt, duration);
                break;

            case "2":
                Console.Write("Id запису: ");
                int cancelId = int.Parse(Console.ReadLine()!);
                Console.Write("Причина (Enter = без причини): ");
                string reason = Console.ReadLine()!;
                clinic.Appointments.Cancel(cancelId, reason);
                break;

            case "3":
                Console.Write("Id запису: ");
                int completeId = int.Parse(Console.ReadLine()!);
                clinic.Appointments.Complete(completeId);
                break;

            case "4":
                Console.Write("Id пацієнта: ");
                int byPatientId = int.Parse(Console.ReadLine()!);
                clinic.Appointments.DisplayList(clinic.Appointments.GetByPatient(byPatientId));
                break;

            case "5":
                Console.Write("Id лікаря: ");
                int byDoctorId = int.Parse(Console.ReadLine()!);
                clinic.Appointments.DisplayList(clinic.Appointments.GetByDoctor(byDoctorId));
                break;

            case "6":
                Console.Write("Дата (dd.MM.yyyy): ");
                DateTime byDate = DateTime.Parse(Console.ReadLine()!);
                clinic.DisplaySchedule(byDate);
                break;

            case "7":
                clinic.Appointments.DisplayList(clinic.Appointments.GetUpcoming());
                break;

            case "8":
                clinic.GenerateReport();
                break;

            case "0":
                exit = true;
                break;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}