using System;

public static class Task7
{
    public static void Run()
    {
        Console.Write("Enter the number of appointments: ");
        int n = int.Parse(Console.ReadLine());

        decimal[] costs=new decimal[n];

        for (int i=0;i<n;i++)
        {
            Console.Write($"Enter the cost of appointment #{i+1}: ");
            costs[i]=decimal.Parse(Console.ReadLine());
        }

        decimal sum=0;
        decimal min=costs[0];
        decimal max=costs[0];

        foreach(decimal cost in costs)
        {
            sum+=cost;

            if(cost<min)
                min=cost;

            if(cost>max)
                max=cost;
        }

        decimal avg=sum/n;

        int aboveAverage=0;

        for(int i=0;i<n;i++)
        {
            if (costs[i]>avg)
                aboveAverage++;
        }

        int iWhile=0;
        int firstOver1000=-1;

        while(iWhile<n)
        {
            if(costs[iWhile]>1000)
            {
                firstOver1000=iWhile;
                break;
            }
            iWhile++;
        }

        Console.WriteLine();
        Console.WriteLine("=== Report ===");
        Console.WriteLine($"Number of appointments: {n}");
        Console.WriteLine($"Total amount: {sum:F2} UAH");
        Console.WriteLine($"Average appointment cost: {avg:F2} UAH");
        Console.WriteLine($"Minimum amount: {min:F2} UAH");
        Console.WriteLine($"Maximum amount: {max:F2} UAH");
        Console.WriteLine($"More expensive than average: {aboveAverage} of {n}");

        if (firstOver1000==-1)
        {
            Console.WriteLine("First appointment over 1000: none");
        }
        else
        {
            Console.WriteLine($"First appointment over 1000: #{firstOver1000+1} ({costs[firstOver1000]:F2} UAH)");
        }
    }
}
