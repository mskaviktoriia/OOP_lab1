using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter medical card number: ");
        int cardNumber=int.Parse(Console.ReadLine());

        if(cardNumber<0)
        {
            Console.WriteLine("Invalid card number!");
            return;
        }

        int lastDigit=cardNumber%10;
        string department;

        switch(lastDigit)
        {
            case 0:
            case 1:
                department="General Therapy";
                break;
            case 2:
            case 3:
                department="Surgery";
                break;
            case 4:
            case 5:
                department="Cardiology";
                break;
            case 6:
            case 7:
                department="Neurology";
                break;
            case 8:
            case 9:
                department="Ophthalmology";
                break;
            default:
                department="Unknown";
                break;
        }

        string discountCard=(cardNumber%2==0)?"Yes":"No";
        string routineCheckup =(cardNumber%3==0)?"Yes":"No";

        Console.WriteLine($"Department: {department}");
        Console.WriteLine($"Discount card: {discountCard}");
        Console.WriteLine($"Routine checkup: {routineCheckup}");
    }
}