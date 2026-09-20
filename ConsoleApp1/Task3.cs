using System;

namespace ConsoleApp1
{
    internal class Task3
    {
        public static void Run()
        {
            string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            int[] patients = new int[7];

            for (int i = 0; i < 7; i++)
            {
                Console.Write("Patients on " + days[i] + ": ");
                patients[i] = int.Parse(Console.ReadLine());

                while (patients[i] < 0)
                {
                    Console.Write("Can't be negative, try again: ");
                    patients[i] = int.Parse(Console.ReadLine());
                }
            }

            int total = 0;
            int busiest = 0;
            int quietest = 0;

            for (int i = 0; i < 7; i++)
            {
                total += patients[i];

                if (patients[i] > patients[busiest])
                    busiest = i;

                if (patients[i] < patients[quietest])
                    quietest = i;
            }

            Console.WriteLine();
            Console.WriteLine("Day\tPatients");
            Console.WriteLine("-----------------");
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine(days[i] + "\t" + patients[i]);
            }
            Console.WriteLine("-----------------");

            Console.WriteLine("Total patients: " + total);
            Console.WriteLine("Busiest day: " + days[busiest] + " (" + patients[busiest] + ")");
            Console.WriteLine("Quietest day: " + days[quietest] + " (" + patients[quietest] + ")");
        }
    }
}