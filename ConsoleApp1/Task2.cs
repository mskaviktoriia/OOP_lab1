using System;

namespace ConsoleApp1
{
    internal class Task2
    {
        public static void Run()
        {
            int n;

            Console.Write("Enter number of appointments: ");
            n = int.Parse(Console.ReadLine());

            while (n <= 0)
            {
                Console.Write("N must be positive, try again: ");
                n = int.Parse(Console.ReadLine());
            }

            double[] costs = new double[n];

            // read costs
            for (int i = 0; i < n; i++)
            {
                Console.Write("Cost of appointment " + (i + 1) + ": ");
                costs[i] = double.Parse(Console.ReadLine());
            }

            // bubble sort, ascending
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (costs[j] > costs[j + 1])
                    {
                        double temp = costs[j];
                        costs[j] = costs[j + 1];
                        costs[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Sorted costs:");
            for (int i = 0; i < n; i++)
            {
                Console.Write(costs[i] + " ");
            }
            Console.WriteLine();
        }
    }
}