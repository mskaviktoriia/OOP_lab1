using ClinicApp;

Patient p1 = new Patient("Іван", "Петренко", new DateTime(1985, 3, 12), "A+", "0501234567");
Patient p2 = new Patient("Олена", "Коваль", new DateTime(1993, 7, 4), "B-", "0672345678");
Patient p3 = new Patient("Максим", "Бойко", new DateTime(2010, 1, 20), "O+", "0933456789");

Patient p4 = new Patient("Марія", "Ткач");

Patient p5 = new Patient();

Patient[] patients = { p1, p2, p3, p4, p5 };

foreach (var p in patients)
{
    Console.WriteLine(p);
}