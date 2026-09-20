using System;

namespace ConsoleApp1
{
    internal class Task4
    {
        public static void Run()
        {
            int n;
            int m;

            Console.Write("Enter number of doctors (N): ");
            n = int.Parse(Console.ReadLine());
            while (n <= 0)
            {
                Console.Write("N must be positive, try again: ");
                n = int.Parse(Console.ReadLine());
            }

            Console.Write("Enter number of working days (M): ");
            m = int.Parse(Console.ReadLine());
            while (m <= 0)
            {
                Console.Write("M must be positive, try again: ");
                m = int.Parse(Console.ReadLine());
            }

            int[,] matrix = new int[n, m];

            // read matrix, row = doctor, column = day
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write("Doctor " + (i + 1) + ", day " + (j + 1) + ": ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            }

            // print the matrix
            Console.WriteLine();
            Console.WriteLine("Matrix:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            // sum for each doctor (rows)
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                int rowSum = 0;
                for (int j = 0; j < m; j++)
                {
                    rowSum += matrix[i, j];
                }
                Console.WriteLine("Doctor " + (i + 1) + " total: " + rowSum);
            }

            // sum for each day (columns)
            Console.WriteLine();
            for (int j = 0; j < m; j++)
            {
                int colSum = 0;
                for (int i = 0; i < n; i++)
                {
                    colSum += matrix[i, j];
                }
                Console.WriteLine("Day " + (j + 1) + " total: " + colSum);
            }

            // max value and its position
            int max = matrix[0, 0];
            int maxRow = 0;
            int maxCol = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                        maxRow = i;
                        maxCol = j;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Max value: " + max);
            Console.WriteLine("Position: doctor " + (maxRow + 1) + ", day " + (maxCol + 1));
        }
    }
}