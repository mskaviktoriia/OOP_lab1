using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter systolic pressure: ");
        int sys=int.Parse(Console.ReadLine());

        Console.Write("Enter diastolic pressure: ");
        int dia=int.Parse(Console.ReadLine());

        if (sys<0||dia<0)
        {
            Console.WriteLine("Invalid input!");
            return;
        }

        string category;
        if (sys<120&&dia<80)
            category="normal";
        else if (sys<130&&dia<80)
            category="elevated";
        else if (sys<140||dia<90)
            category="hypertension stage 1";
        else
            category="hypertension stage 2";

        Console.WriteLine($"Category: {category}");
    }
}