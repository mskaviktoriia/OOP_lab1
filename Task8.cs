using System;

public static class Task8
{
    public static void Run()
    {
        // Task 1
        Console.Write("Enter weight(kg): ");
        double weight=double.Parse(Console.ReadLine());

        Console.Write("Enter height(m): ");
        double height=double.Parse(Console.ReadLine());

        double bmi=CalculateBMI(weight, height);
        string bmiCategory=GetBMICategory(bmi);

        // Task 2
        Console.Write("Enter price: ");
        double price=double.Parse(Console.ReadLine());

        Console.Write("Enter count: ");
        int count=int.Parse(Console.ReadLine());

        Console.Write("Enter discount(%): ");
        int discount=int.Parse(Console.ReadLine());

        double totalCost=CalculateCost(price, count, discount);

        // Task 3
        Console.Write("Enter birth year: ");
        int birthYear=int.Parse(Console.ReadLine());

        int age=2026-birthYear;
        string ageCategory=GetAgeCategory(age);


        // Task 4
        Console.Write("Enter systolic pressure: ");
        int sys=int.Parse(Console.ReadLine());

        Console.Write("Enter diastolic pressure: ");
        int dia=int.Parse(Console.ReadLine());

        string pressureStatus=GetPressureStatus(sys, dia);

        Console.WriteLine();
        Console.WriteLine("=== Report ===");

        Console.WriteLine($"BMI: {bmi:F2} - {bmiCategory}");
        Console.WriteLine($"Total: {totalCost:F2} UAH");
        Console.WriteLine($"Age: {age} years, category: {ageCategory}");
        Console.WriteLine($"Blood pressure: {sys}/{dia} - {pressureStatus}");
    }

    static double CalculateBMI(double weight, double height)
    {
        return weight/(height*height);
    }

    static string GetBMICategory(double bmi)
    {
        if (bmi<18.5)
            return "underweight";

        if (bmi<25)
            return "normal weight";

        if (bmi<30)
            return "overweight";

        return "obesity";
    }

    static double CalculateCost(double price, int visits, int discount)
    {
        return price*visits*(1-(double)discount/100);
    }

    static string GetAgeCategory(int age)
    {
        if (age<18)
            return "child";

        if (age<60)
            return "adult";

        return "pensioner";
    }

    static string GetPressureStatus(int systolic, int diastolic)
    {
        if (systolic<120&&diastolic<80)
            return "normal";

        if (systolic<130&&diastolic<80)
            return "elevated";

        if (systolic<140||diastolic<90)
            return "hypertension stage 1";

        return "hypertension stage 2";
    }
}
