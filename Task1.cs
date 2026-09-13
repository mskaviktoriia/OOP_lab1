using System;

public static class Task1
{
    public static void Run()
    {
        Console.Write("Enter weight(kg): ");
        double weight=double.Parse(Console.ReadLine());

        Console.Write("Enter height(m): ");
        double height=double.Parse(Console.ReadLine());

        double bmi=CalculateBMI(weight, height);
        string category=GetBMICategory(bmi);

        Console.WriteLine($"BMI: {bmi:F2}");
        Console.WriteLine($"Category: {category}");
    }

    public static double CalculateBMI(double weight, double height)
    {
        return weight/(height*height);
    }

    public static string GetBMICategory(double bmi)
    {
        if(bmi<18.5)
            return "underweight";
        if(bmi<25)
            return "normal weight";
        if(bmi<30)
            return "overweight";
        return "obesity";
    }
}