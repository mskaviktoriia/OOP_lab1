using System;

namespace ConsoleApp1
{
    internal class Task6
    {
        public static void Run()
        {
            int n;

            Console.Write("Enter number of doctors (N): ");
            n = int.Parse(Console.ReadLine());

            while (n <= 0)
            {
                Console.Write("N must be positive, try again: ");
                n = int.Parse(Console.ReadLine());
            }

            // jagged array: every doctor has his own array of costs
            double[][] costs = new double[n][];

            for (int i = 0; i < n; i++)
            {
                Console.Write("Number of appointments for doctor " + (i + 1) + ": ");
                int count = int.Parse(Console.ReadLine());

                while (count < 0)
                {
                    Console.Write("Can't be negative, try again: ");
                    count = int.Parse(Console.ReadLine());
                }

                costs[i] = new double[count];

                for (int j = 0; j < count; j++)
                {
                    Console.Write("  Cost of appointment " + (j + 1) + ": ");
                    costs[i][j] = double.Parse(Console.ReadLine());
                }
            }

            double[] income = new double[n];

            // statistics for each doctor
            Console.WriteLine();
            Console.WriteLine("Statistics:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Doctor " + (i + 1) + ":");

                if (costs[i].Length == 0)
                {
                    Console.WriteLine("  no appointments");
                    continue;
                }

                double sum = 0;
                double min = costs[i][0];
                double max = costs[i][0];

                for (int j = 0; j < costs[i].Length; j++)
                {
                    sum += costs[i][j];

                    if (costs[i][j] < min)
                        min = costs[i][j];

                    if (costs[i][j] > max)
                        max = costs[i][j];
                }

                income[i] = sum;
                double avg = sum / costs[i].Length;

                Console.WriteLine("  Appointments: " + costs[i].Length);
                Console.WriteLine("  Income: " + sum);
                Console.WriteLine("  Average cost: " + avg);
                Console.WriteLine("  Min cost: " + min);
                Console.WriteLine("  Max cost: " + max);
            }

            // doctor with the biggest income
            int best = 0;
            for (int i = 1; i < n; i++)
            {
                if (income[i] > income[best])
                    best = i;
            }

            Console.WriteLine();
            Console.WriteLine("Top doctor: " + (best + 1) + " with income " + income[best]);
        }
    }
}