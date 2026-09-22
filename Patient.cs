using System;

namespace ConsoleApp1
{
    public class Patient
    {
        private static int idCounter = 1;

        public int Id { get; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Diagnosis { get; set; }
        public DateTime AdmissionDate { get; }

        // obchisluvani vlastyvosti
        public bool IsAdult => Age >= 18;
        public int DaysInClinic => (DateTime.Now - AdmissionDate).Days;
        public string ShortInfo => $"#{Id} {FullName} ({Age} y.o.)";

        // constructor 1 - main
        public Patient(string fullName, int age, string diagnosis)
        {
            Id = idCounter++;
            FullName = fullName;
            Age = age;
            Diagnosis = diagnosis;
            AdmissionDate = DateTime.Now;
        }

        // constructor 2 - not found diagnos
        public Patient(string fullName, int age ) : this(fullName, age, "Not diagnosed")
        {

        }

        // constructor 3 - empty
        public Patient() : this("Unknown", 0)
        {
        }

        // patient clasification metod
        public string Classify()
        {
            if (Age < 18)
                return "Child";
            if (Age < 65)
                return "Adult";
            return "Senior";
        }

        public override string ToString()
        {
            return "Id=" + Id + ", Name=" + FullName + ", Age=" + Age + ", Diagnosis=" + Diagnosis +
                ", IsAdult=" + IsAdult + ", DaysInClinic=" + DaysInClinic + ", Category=" + Classify() +
                ", AdmissionDate=" + AdmissionDate;
        }
    }
}