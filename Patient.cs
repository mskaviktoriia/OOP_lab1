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
        public BloodType BloodType { get; set; }
        public DateTime AdmissionDate { get; }

        public bool IsAdult => Age >= 18;
        public int DaysInClinic => (DateTime.Now - AdmissionDate).Days;
        public string ShortInfo => $"#{Id} {FullName} ({Age} y.o.)";

        public Patient(string fullName, int age, string diagnosis, BloodType bloodType = BloodType.Unknown)
        {
            Id = idCounter++;
            FullName = fullName;
            Age = age;
            Diagnosis = diagnosis;
            BloodType = bloodType;
            AdmissionDate = DateTime.Now;
        }

        public Patient(string fullName, int age) : this(fullName, age, "Not diagnosed")
        {
        }

        public Patient() : this("Unknown", 0)
        {
        }

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
                ", BloodType=" + BloodType + ", IsAdult=" + IsAdult + ", DaysInClinic=" + DaysInClinic +
                ", Category=" + Classify() + ", AdmissionDate=" + AdmissionDate;
        }
    }
}