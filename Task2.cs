using System;

public static class Task2
{
    public static void Run()
    {
        Console.Write("Enter price: ");
        double price = double.Parse(Console.ReadLine());

        Console.Write("Enter count: ");
        int count = int.Parse(Console.ReadLine());

        Console.Write("Enter discount(%): ");
        int discount = int.Parse(Console.ReadLine());

        double total = CalculateCost(price, count, discount);

        Console.WriteLine($"Sum: {total:F2} grn");
    }

    public static double CalculateCost(double price, int count, int discount)
    {
        return price*count*(1-(double)discount/100);
    }
}