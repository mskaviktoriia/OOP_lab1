using System;

namespace ConsoleApp1
{
    public class Appointment
    {
        private static int idCounter = 1; // id counter

        public int Id { get; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; private set; } // Scheduled, Cancelled, Completed

        public Appointment(int patientId, int doctorId, DateTime date)
        {
            Id = idCounter++;
            PatientId = patientId;
            DoctorId = doctorId;
            Date = date;
            Status = "Scheduled";
        }

        // move to Cancelled, only from Scheduled
        public bool Cancel()
        {
            if (Status != "Scheduled")
            {
                Console.WriteLine("Cannot cancel, status is already " + Status);
                return false;
            }

            Status = "Cancelled";
            return true;
        }

        // move to Completed, only from Scheduled
        public bool Complete()
        {
            if (Status != "Scheduled")
            {
                Console.WriteLine("Cannot complete, status is already " + Status);
                return false;
            }

            Status = "Completed";
            return true;
        }

        public override string ToString()
        {
            return "Id=" + Id + ", PatientId=" + PatientId + ", DoctorId=" + DoctorId +
                ", Date=" + Date.ToString("g") + ", Status=" + Status;
        }
    }
}