using System;

public static class Task6
{
    public static void Run()
    {
        Console.Write("Enter medical card number: ");
        int cardNumber=int.Parse(Console.ReadLine());

        int lastDigit=cardNumber%10;

        string department=lastDigit switch
        {
            0 or 1 => "General Therapy",
            2 or 3 => "Surgery",
            4 or 5 => "Cardiology",
            6 or 7 => "Neurology",
            8 or 9 => "Ophthalmology",
            _ => "Unknown Department"
        };

        string discountCard=(cardNumber%2==0)?"Yes":"No";
        string routineCheckup=(cardNumber%3==0)?"Yes":"No";

        Console.WriteLine($"Department: {department}");
        Console.WriteLine($"Discount card: {discountCard}");
        Console.WriteLine($"Routine checkup: {routineCheckup}");
    }
}