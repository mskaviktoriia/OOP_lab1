using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 5 patient with different constructors
            Patient p1 = new Patient("Ivan Petrenko", 34, "Flu");
            Patient p2 = new Patient("Olena Kovalchuk", 45);
            Patient p3 = new Patient();
            Patient p4 = new Patient("Mykola Sydorenko", 70, "Hypertension");
            Patient p5 = new Patient("Anna Bondar", 16);

            Patient[] patients = { p1, p2, p3, p4, p5 };

            foreach (var p in patients)
            {
                Console.WriteLine(p);
            }

        }
    }
}