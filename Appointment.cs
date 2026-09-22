using System;

namespace ConsoleApp1
{
    public class Appointment
    {
        private static int _nextId = 1; // id counter

        public int Id { get; }
        public int PatientId { get; }
        public int DoctorId { get; }
        public DateTime ScheduledAt { get; set; }
        public int DurationMinutes { get; set; }
        public string Status { get; private set; } // Scheduled, Cancelled, Completed
        public string Notes { get; private set; }

        // computed properties
        public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);
        public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == "Scheduled";

        public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
        {
            Id = _nextId++;
            PatientId = patientId;
            DoctorId = doctorId;
            ScheduledAt = scheduledAt;
            DurationMinutes = durationMinutes;
            Status = "Scheduled";
            Notes = "";
        }

        // move Scheduled -> Cancelled
        public bool Cancel(string reason = "")
        {
            if (Status != "Scheduled")
                return false;

            Status = "Cancelled";
            if (reason.Length > 0)
                Notes = reason;

            return true;
        }

        // move Scheduled -> Completed
        public bool Complete()
        {
            if (Status != "Scheduled")
                return false;

            Status = "Completed";
            return true;
        }

        public override string ToString()
        {
            string result = "[" + Id + "] Patient #" + PatientId + " -> Doctor #" + DoctorId +
                " | " + ScheduledAt.ToString("dd.MM.yyyy HH:mm") + "-" + EndsAt.ToString("HH:mm") +
                " | " + Status;

            if (Notes.Length > 0)
                result += " | " + Notes;

            return result;
        }
    }
}