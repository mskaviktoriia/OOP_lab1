using System;

class Task1
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

        double[] weights = new double[n];

        // read weights
        for (int i = 0; i < n; i++)
        {
            Console.Write("Weight of patient " + (i + 1) + ": ");
            weights[i] = double.Parse(Console.ReadLine());
        }

        double sum = 0;
        double min = weights[0];
        double max = weights[0];

        for (int i = 0; i < n; i++)
        {
            sum += weights[i];

            if (weights[i] < min)
                min = weights[i];

            if (weights[i] > max)
                max = weights[i];
        }

        double avg = sum / n;

        // count patients above average
        int count = 0;
        for (int i = 0; i < n; i++)
        {
            if (weights[i] > avg)
                count++;
        }

        Console.WriteLine();
        Console.WriteLine("Average weight: " + avg);
        Console.WriteLine("Min weight: " + min);
        Console.WriteLine("Max weight: " + max);
        Console.WriteLine("Patients above average: " + count);
    }
}