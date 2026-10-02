using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PatientManager patientManager = new PatientManager();
            DoctorManager doctorManager = new DoctorManager();
            AppointmentManager appointmentManager = new AppointmentManager(patientManager, doctorManager);

            // starting data
            patientManager.Add(new Patient("Ivan Petrenko", 34, "Flu", BloodType.APositive));
            patientManager.Add(new Patient("Olena Kovalchuk", 45));
            patientManager.Add(new Patient("Mykola Sydorenko", 70, "Hypertension", BloodType.ONegative));

            doctorManager.Add(new Doctor("Petro Ivanenko", Speciality.General, new WorkSchedule(8, 16)));
            doctorManager.Add(new Doctor("Maria Shevchenko", Speciality.Surgery, new WorkSchedule(10, 18)));
            doctorManager.Add(new Doctor("Oleg Tkachenko", Speciality.Cardiology, new WorkSchedule(12, 20)));

            appointmentManager.Book(1, 1, DateTime.Now.AddDays(1));
            appointmentManager.Book(2, 2, DateTime.Now.AddDays(2), 45);

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

                        Console.WriteLine("Blood type:");
                        for (int i = 0; i < 9; i++)
                            Console.WriteLine(i + ". " + (BloodType)i);
                        Console.Write("Choose: ");
                        BloodType blood = (BloodType)int.Parse(Console.ReadLine());

                        manager.Add(new Patient(name, age, diag, blood));
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

                        Console.WriteLine("Speciality:");
                        for (int i = 0; i < 8; i++)
                            Console.WriteLine(i + ". " + (Speciality)i);
                        Console.Write("Choose: ");
                        Speciality spec = (Speciality)int.Parse(Console.ReadLine());

                        Console.Write("Start hour: ");
                        int start = int.Parse(Console.ReadLine());
                        Console.Write("End hour: ");
                        int end = int.Parse(Console.ReadLine());

                        manager.Add(new Doctor(name, spec, new WorkSchedule(start, end)));
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
                        break;

                    case "2":
                        Console.Write("Appointment id: ");
                        int cancelId = int.Parse(Console.ReadLine());
                        Console.Write("Reason (empty = none): ");
                        string reason = Console.ReadLine();

                        if (manager.Cancel(cancelId, reason))
                            Console.WriteLine("Appointment [" + cancelId + "] cancelled.");
                        else
                            Console.WriteLine("Not found or already final.");
                        break;

                    case "3":
                        Console.Write("Appointment id: ");
                        int compId = int.Parse(Console.ReadLine());

                        if (manager.Complete(compId))
                            Console.WriteLine("Appointment [" + compId + "] completed.");
                        else
                            Console.WriteLine("Not found or already final.");
                        break;

                    case "4":
                        Console.Write("Patient id: ");
                        int byPatientId = int.Parse(Console.ReadLine());
                        manager.DisplayList(manager.GetByPatient(byPatientId));
                        break;

                    case "5":
                        Console.Write("Doctor id: ");
                        int byDoctorId = int.Parse(Console.ReadLine());
                        manager.DisplayList(manager.GetByDoctor(byDoctorId));
                        break;

                    case "6":
                        Console.Write("Date (dd.MM.yyyy): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());
                        manager.DisplayList(manager.GetByDate(date));
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
}