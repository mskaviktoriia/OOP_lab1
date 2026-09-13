using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter price: ");
        double price=double.Parse(Console.ReadLine());

        Console.Write("Enter count: ");
        int count=int.Parse(Console.ReadLine());

        Console.Write("Enter discount(%): ");
        double discount=double.Parse(Console.ReadLine());

        double total=price*count*(1-discount/100);

        Console.WriteLine($"Total: {total:F2}");
    }
}