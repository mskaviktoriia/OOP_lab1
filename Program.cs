using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter weight(kg): ");
        double weight=double.Parse(Console.ReadLine());

        Console.Write("Enter height (m): ");
        double height=double.Parse(Console.ReadLine());

        double bmi=weight/(height*height);

        Console.WriteLine($"BMI: {bmi:F2}");
    }
}