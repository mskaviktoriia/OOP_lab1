using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter day number (1-7): ");
        int day=int.Parse(Console.ReadLine());

        string dayName;
        string hours;

        switch (day)
        {
            case 1:
                dayName="Monday";
                hours ="08:00–18:00";
                break;
            case 2:
                dayName="Tuesday";
                hours="08:00–18:00";
                break;
            case 3:
                dayName="Wednesday";
                hours="09:00–17:00";
                break;
            case 4:
                dayName="Thursday";
                hours="08:00–18:00";
                break;
            case 5:
                dayName="Friday";
                hours="08:00–16:00";
                break;
            case 6:
                dayName="Saturday";
                hours="09:00–14:00";
                break;
            case 7:
                dayName="Sunday";
                hours="Day off";
                break;
            default:
                Console.WriteLine("Invalid day number!");
                return;
        }

        Console.WriteLine($"Day: {dayName}, Hours: {hours}");
    }
}