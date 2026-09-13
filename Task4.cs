using System;

public static class Task4
{
    public static void Run()
    {
        Console.Write("Enter systolic pressure: ");
        int sys=int.Parse(Console.ReadLine());

        Console.Write("Enter diastolic pressure: ");
        int dia=int.Parse(Console.ReadLine());

        string category=GetPressureStatus(sys, dia);

        Console.WriteLine($"Your pressure: {sys}/{dia} - {category}");
    }

    public static string GetPressureStatus(int sys, int dia)
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