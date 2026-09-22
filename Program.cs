using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Doctor d1 = new Doctor("Petro Ivanenko", "Therapist", 8, 16);
            Doctor d2 = new Doctor("Maria Shevchenko", "Surgeon", 10, 18);
            Doctor d3 = new Doctor("Oleg Tkachenko", "Cardiologist", 12, 20);
            Doctor d4 = new Doctor("Nina Boyko", "Pediatrician", 9, 15);

            Doctor[] doctors = { d1, d2, d3, d4 };

            int currentHour = DateTime.Now.Hour; // current hour

            foreach (var d in doctors)
            {
                Console.WriteLine(d);
            }

            Console.WriteLine();
            Console.WriteLine("Current hour: " + currentHour);
            Console.WriteLine("Available now:");

            foreach (var d in doctors)
            {
                if (d.IsAvailable(currentHour))
                    Console.WriteLine(d.FullName);
            }

        }
    }
}