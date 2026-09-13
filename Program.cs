using System;

class Program
{
    static void Main()
    {
        Run();
    }

    static void Run()
    {
        // Task 1
        Console.Write("Enter weight(kg): ");
        double weight=double.Parse(Console.ReadLine());
        Console.Write("Enter height(m): ");
        double height=double.Parse(Console.ReadLine());

        double bmi=CalculateBMI(weight, height);
        string bmiCategory=GetBMICategory(bmi);
        Console.WriteLine($"BMI: {bmi:F2}, Category: {bmiCategory}");

        // Task 2
        Console.Write("Enter price: ");
        double price=double.Parse(Console.ReadLine());
        Console.Write("Enter count: ");
        int count= int.Parse(Console.ReadLine());
        Console.Write("Enter discount(%): ");
        int discount=int.Parse(Console.ReadLine());

        double totalCost =CalculateCost(price, count, discount);
        Console.WriteLine($"Total cost: {totalCost:F2}");

        // Task 3
        Console.Write("Enter birth year: ");
        int birthYear=int.Parse(Console.ReadLine());

        int age=2026-birthYear;
        string ageCategory=GetAgeCategory(age);
        Console.WriteLine($"Age: {age}, Category: {ageCategory}") ;

        // Task 4
        Console.Write("Enter systolic pressure: ");
        int sys=int.Parse(Console.ReadLine());
        Console.Write("Enter diastolic pressure: ");
        int dia=int.Parse(Console.ReadLine());

        string pressureStatus=GetPressureStatus(sys, dia);
        Console.WriteLine($"Pressure status: {pressureStatus}");
    }

    static double CalculateBMI(double weight, double height)
    {
        return weight/(height*height);
    }

    static string GetBMICategory(double bmi)
    {
        if(bmi<18.5)
            return "underweight";
        if(bmi<25)
            return "normal weight";
        if(bmi<30)
            return "overweight";
        return "obesity";
    }

    static double CalculateCost(double price, int count, int discount)
    {
        return price*count*(1-(double)discount/100);
    }

    static string GetAgeCategory(int age)
    {
        if(age<18)
            return "child";
        if(age<60)
            return "adult";
        return "old";
    }

    static string GetPressureStatus(int sys, int dia)
    {
        if(sys<120&&dia<80)
            return "normal";
        if(sys<130&&dia<80)
            return "elevated";
        if(sys<140||dia<90)
            return "hypertension stage 1";
        return "hypertension stage 2";
    }
}