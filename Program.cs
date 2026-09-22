using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Appointment a1 = new Appointment(1, 1, DateTime.Now.AddDays(1));
            Appointment a2 = new Appointment(2, 2, DateTime.Now.AddDays(2));
            Appointment a3 = new Appointment(3, 1, DateTime.Now.AddDays(3));

            Console.WriteLine(a1);
            Console.WriteLine(a2);
            Console.WriteLine(a3);

            Console.WriteLine();

            // complete first one
            a1.Complete();
            Console.WriteLine(a1);

            // cancel second one
            a2.Cancel();
            Console.WriteLine(a2);

            Console.WriteLine();

            // try to change status again - should fail
            a1.Cancel();
            a2.Complete();

            Console.WriteLine();
            Console.WriteLine(a1);
            Console.WriteLine(a2);
            Console.WriteLine(a3); // still Scheduled
        }
    }
}