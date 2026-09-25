using ClinicApp;

Doctor d1 = new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567");
d1.WorkStartHour = 8;
d1.WorkEndHour = 16;

Doctor d2 = new Doctor("Наталія", "Мороз", "Неврологія", "LIC-002", "0442345678");
d2.WorkStartHour = 9;
d2.WorkEndHour = 18;

Doctor d3 = new Doctor("Андрій", "Власенко", "Педіатрія");

Doctor d4 = new Doctor();

Doctor[] doctors = { d1, d2, d3, d4 };

foreach (var d in doctors)
{
    Console.WriteLine(d);
}


PatientManager patientManager = new PatientManager();

patientManager.Add(new Patient("Іван", "Петренко", new DateTime(1985, 3, 12), "A+", "0501234567"));
patientManager.Add(new Patient("Олена", "Коваль", new DateTime(1993, 7, 4), "B-", "0672345678"));
patientManager.Add(new Patient("Максим", "Бойко", new DateTime(2010, 1, 20), "O+", "0933456789"));
patientManager.Add(new Patient("Марія", "Ткач"));

PatientsMenu();


void PatientsMenu()
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
                patientManager.DisplayAll();
                break;

            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine()!;
                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine()!;
                patientManager.Add(new Patient(firstName, lastName));
                break;

            case "3":
                Console.Write("Ім'я або прізвище для пошуку: ");
                string query = Console.ReadLine()!;
                Patient[] found = patientManager.FindByName(query);
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
                bool removed = patientManager.Remove(removeId);
                Console.WriteLine(removed ? "Видалено." : "Пацієнта з таким Id не знайдено.");
                break;

            case "5":
                patientManager.DisplayStats();
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