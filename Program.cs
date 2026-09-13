using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of appointments: ");
        int n=int.Parse(Console.ReadLine());

        if (n<= 0)
        {
            Console.WriteLine("Invalid number of appointments!");
            return;
        }

        double total=0;
        double min=double.MaxValue;
        double max=double.MinValue;

        for(int i=0;i<n;i++)
        {
            Console.Write($"Enter cost {i+1}: ");
            double cost=double.Parse(Console.ReadLine());

            total+=cost;

            if(cost<min)
                min=cost;

            if (cost>max)
                max=cost;
        }

        double avg=total/n;

        Console.WriteLine($"Total: {total:F2}");
        Console.WriteLine($"Average: {avg:F2}");
        Console.WriteLine($"Min: {min:F2}");
        Console.WriteLine($"Max: {max:F2}");
    }
}