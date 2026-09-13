using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter birth year: ");
        int birthYear=int.Parse(Console.ReadLine());

        int age=2026-birthYear;

        if (age<0)
        {
            Console.WriteLine("Invalid year!");
            return;
        }

        string category;
        if (age<18)
            category="child";
        else if (age<60)
            category="adult";
        else
            category="old";

        Console.WriteLine($"Age: {age}, Category: {category}");
    }
}