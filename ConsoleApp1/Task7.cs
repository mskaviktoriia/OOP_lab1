using System;

namespace ConsoleApp1
{
    internal class Task7
    {
        public static void Run()
        {
            int n;

            Console.Write("Enter number of patients: ");
            n = int.Parse(Console.ReadLine());

            while (n <= 0)
            {
                Console.Write("N must be positive, try again: ");
                n = int.Parse(Console.ReadLine());
            }

            string[] names = new string[n];
            double[] bmi = new double[n];

            // read name and BMI for each patient
            for (int i = 0; i < n; i++)
            {
                Console.Write("Name of patient " + (i + 1) + ": ");
                names[i] = Console.ReadLine();

                Console.Write("BMI of " + names[i] + ": ");
                bmi[i] = double.Parse(Console.ReadLine());

                while (bmi[i] <= 0)
                {
                    Console.Write("BMI must be positive, try again: ");
                    bmi[i] = double.Parse(Console.ReadLine());
                }
            }

            // bubble sort, descending by BMI
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (bmi[j] < bmi[j + 1])
                    {
                        // swap BMI
                        double tempBmi = bmi[j];
                        bmi[j] = bmi[j + 1];
                        bmi[j + 1] = tempBmi;

                        // swap names too, so they stay in sync
                        string tempName = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = tempName;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Ranking by BMI (highest first):");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine((i + 1) + ". " + names[i] + " - " + bmi[i]);
            }
        }
    }
}