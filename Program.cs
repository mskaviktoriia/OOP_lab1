using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Appointment a1 = new Appointment(1, 1, new DateTime(2026, 5, 9, 10, 0, 0));
            Appointment a2 = new Appointment(2, 2, new DateTime(2026, 5, 9, 11, 0, 0), 45);
            Appointment a3 = new Appointment(3, 3, new DateTime(2026, 5, 10, 9, 0, 0), 20);

            Console.WriteLine(a1);
            Console.WriteLine(a2);
            Console.WriteLine(a3);

            Console.WriteLine();

            a1.Cancel("Patient could not come"); // cancel with reason
            a2.Complete();

            Console.WriteLine(a1);
            Console.WriteLine(a2);
            Console.WriteLine(a3); // still Scheduled

            Console.WriteLine();

            // try again - should fail, already final
            bool r1 = a1.Complete();
            bool r2 = a2.Cancel();

            Console.WriteLine("a1.Complete() result: " + r1);
            Console.WriteLine("a2.Cancel() result: " + r2);
        }
    }
}