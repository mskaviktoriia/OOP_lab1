using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PatientManager manager = new PatientManager();

            // add some starting patients
            manager.Add(new Patient("Ivan Petrenko", 34, "Flu"));
            manager.Add(new Patient("Olena Kovalchuk", 45));
            manager.Add(new Patient("Mykola Sydorenko", 70, "Hypertension"));

            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== Patients menu ===");
                Console.WriteLine("1. Show all");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Find by name");
                Console.WriteLine("4. Remove");
                Console.WriteLine("5. Statistics");
                Console.WriteLine("0. Exit");
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
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Wrong option.");
                        break;
                }
            }

        }
    }
}