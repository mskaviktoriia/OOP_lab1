using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PatientManager patientManager = new PatientManager();
            DoctorManager doctorManager = new DoctorManager();

            // starting data
            patientManager.Add(new Patient("Ivan Petrenko", 34, "Flu"));
            patientManager.Add(new Patient("Olena Kovalchuk", 45));
            patientManager.Add(new Patient("Mykola Sydorenko", 70, "Hypertension"));

            doctorManager.Add(new Doctor("Petro Ivanenko", "Therapist", 8, 16));
            doctorManager.Add(new Doctor("Maria Shevchenko", "Surgeon", 10, 18));
            doctorManager.Add(new Doctor("Oleg Tkachenko", "Cardiologist", 12, 20));

            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== Main menu ===");
                Console.WriteLine("1. Patients");
                Console.WriteLine("2. Doctors");
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
                Console.WriteLine("0. Back");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.ShowAll();
                        break;

                    case "2":
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Age: ");
                        int age = int.Parse(Console.ReadLine());
                        Console.Write("Diagnosis: ");
                        string diag = Console.ReadLine();

                        manager.Add(new Patient(name, age, diag));
                        Console.WriteLine("Added.");
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
                        Console.Write("Id to remove: ");
                        int id = int.Parse(Console.ReadLine());

                        if (manager.Remove(id))
                            Console.WriteLine("Removed.");
                        else
                            Console.WriteLine("Not found.");
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
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Speciality: ");
                        string spec = Console.ReadLine();
                        Console.Write("Start hour: ");
                        int start = int.Parse(Console.ReadLine());
                        Console.Write("End hour: ");
                        int end = int.Parse(Console.ReadLine());

                        manager.Add(new Doctor(name, spec, start, end));
                        Console.WriteLine("Added.");
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
                        Console.Write("Id to remove: ");
                        int id = int.Parse(Console.ReadLine());

                        if (manager.Remove(id))
                            Console.WriteLine("Removed.");
                        else
                            Console.WriteLine("Not found.");
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
    }
}