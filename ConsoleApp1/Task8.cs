using System;

namespace ConsoleApp1
{
    internal class Task8
    {
        public static void Run()
        {
            int d;
            int w;

            Console.Write("Enter number of departments: ");
            d = int.Parse(Console.ReadLine());
            while (d <= 0)
            {
                Console.Write("Must be positive, try again: ");
                d = int.Parse(Console.ReadLine());
            }

            Console.Write("Enter number of weeks: ");
            w = int.Parse(Console.ReadLine());
            while (w <= 0)
            {
                Console.Write("Must be positive, try again: ");
                w = int.Parse(Console.ReadLine());
            }

            string[] shiftNames = { "morning", "evening" };

            // [department][week][shift], shift 0 = morning, 1 = evening
            int[,,] data = new int[d, w, 2];

            // read data
            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        Console.Write("Department " + (i + 1) + ", week " + (j + 1) + ", " + shiftNames[k] + " shift: ");
                        data[i, j, k] = int.Parse(Console.ReadLine());

                        while (data[i, j, k] < 0)
                        {
                            Console.Write("Can't be negative, try again: ");
                            data[i, j, k] = int.Parse(Console.ReadLine());
                        }
                    }
                }
            }

            int[] deptTotal = new int[d];

            // analytics for each department
            Console.WriteLine();
            Console.WriteLine("Analytics:");
            for (int i = 0; i < d; i++)
            {
                int morning = 0;
                int evening = 0;
                int bestWeek = 0;
                int bestWeekSum = -1;

                for (int j = 0; j < w; j++)
                {
                    morning += data[i, j, 0];
                    evening += data[i, j, 1];

                    int weekSum = data[i, j, 0] + data[i, j, 1];
                    if (weekSum > bestWeekSum)
                    {
                        bestWeekSum = weekSum;
                        bestWeek = j;
                    }
                }

                int total = morning + evening;
                deptTotal[i] = total;
                double avgPerWeek = (double)total / w;

                Console.WriteLine();
                Console.WriteLine("Department " + (i + 1) + ":");
                Console.WriteLine("  Morning shifts total: " + morning);
                Console.WriteLine("  Evening shifts total: " + evening);
                Console.WriteLine("  Total: " + total);
                Console.WriteLine("  Average per week: " + avgPerWeek);
                Console.WriteLine("  Busiest week: " + (bestWeek + 1) + " (" + bestWeekSum + ")");
            }

            // busiest department
            int best = 0;
            for (int i = 1; i < d; i++)
            {
                if (deptTotal[i] > deptTotal[best])
                    best = i;
            }

            Console.WriteLine();
            Console.WriteLine("Busiest department: " + (best + 1) + " with " + deptTotal[best] + " patients");
        }
    }
}