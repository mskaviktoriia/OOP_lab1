using System;

namespace ConsoleApp1
{
    public class Doctor
    {
        private static int idCounter = 1; // id counter

        public int Id { get; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }

        public Doctor(string fullName, string specialty, int startHour, int endHour)
        {
            Id = idCounter++;
            FullName = fullName;
            Specialty = specialty;
            StartHour = startHour;
            EndHour = endHour;
        }

        // check if doctor works at given hour
        public bool IsAvailable(int hour)
        {
            if (hour < 0 || hour > 23)
                return false;

            return hour >= StartHour && hour < EndHour;
        }

        public override string ToString()
        {
            return "Id=" + Id + ", Name=" + FullName + ", Specialty=" + Specialty +
                ", Schedule=" + StartHour + "-" + EndHour;
        }
    }
}