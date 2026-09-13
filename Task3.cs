using System;

public static class Task3
{
    public static void Run()
    {
        Console.Write("Enter birth year: ");
        int birthYear=int.Parse(Console.ReadLine());

        int age=2026-birthYear;
        string category = GetAgeCategory(age);

        Console.WriteLine($"Age: {age} y.");
        Console.WriteLine($"Category: {category}");
    }

    public static string GetAgeCategory(int age)
    {
        if (age<18)
            return "child";
        if (age<60)
            return "adult";
        return "old";
    }
}