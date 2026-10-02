using System;

namespace ConsoleApp1
{
    public class Doctor
    {
        private static int idCounter = 1;

        public int Id { get; }
        public string FullName { get; set; }
        public Speciality Specialty { get; set; }
        public WorkSchedule Schedule { get; set; }

        public bool IsAvailableNow => Schedule.IsNow;

        public Doctor(string fullName, Speciality specialty, WorkSchedule schedule)
        {
            Id = idCounter++;
            FullName = fullName;
            Specialty = specialty;
            Schedule = schedule;
        }

        public bool IsAvailable(int hour)
        {
            if (hour < 0 || hour > 23)
                return false;

            return Schedule.Contains(hour);
        }

        public override string ToString()
        {
            return "Id=" + Id + ", Name=" + FullName + ", Specialty=" + ClinicFormatter.FormatSpeciality(Specialty) +
                ", Schedule=" + Schedule;
        }
    }
}