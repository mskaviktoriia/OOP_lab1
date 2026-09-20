using System;

namespace ConsoleApp1
{
    internal class Task5
    {
        public static void Run()
        {
            int n;

            Console.Write("Enter size of the matrix (N): ");
            n = int.Parse(Console.ReadLine());

            while (n <= 0)
            {
                Console.Write("N must be positive, try again: ");
                n = int.Parse(Console.ReadLine());
            }

            int[,] matrix = new int[n, n];

            // read matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("Element [" + i + "," + j + "]: ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            }

            // print the matrix
            Console.WriteLine();
            Console.WriteLine("Matrix:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            // main diagonal: i == j
            int mainSum = 0;
            Console.WriteLine();
            Console.Write("Main diagonal: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(matrix[i, i] + " ");
                mainSum += matrix[i, i];
            }
            Console.WriteLine();
            Console.WriteLine("Main diagonal sum: " + mainSum);

            // secondary diagonal: i + j == N - 1
            int secSum = 0;
            Console.WriteLine();
            Console.Write("Secondary diagonal: ");
            for (int i = 0; i < n; i++)
            {
                int j = n - 1 - i;
                Console.Write(matrix[i, j] + " ");
                secSum += matrix[i, j];
            }
            Console.WriteLine();
            Console.WriteLine("Secondary diagonal sum: " + secSum);
        }
    }
}