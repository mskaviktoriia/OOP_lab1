using System;

namespace ConsoleApp1
{
    public class AppointmentManager
    {
        private const int MaxAppointments = 500;

        private Appointment[] _appointments = new Appointment[MaxAppointments];
        private int _count = 0;

        private PatientManager _patients;
        private DoctorManager _doctors;

        public int Count => _count;

        public AppointmentManager(PatientManager patients, DoctorManager doctors)
        {
            _patients = patients;
            _doctors = doctors;
        }

        // book new appointment, validates ids first
        public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
        {
            Patient p = _patients.FindById(patientId);
            if (p == null)
            {
                Console.WriteLine("Error: patient with id " + patientId + " not found.");
                return false;
            }

            Doctor d = _doctors.FindById(doctorId);
            if (d == null)
            {
                Console.WriteLine("Error: doctor with id " + doctorId + " not found.");
                return false;
            }

            if (_count >= MaxAppointments)
            {
                Console.WriteLine("Array is full!");
                return false;
            }

            Appointment a = new Appointment(patientId, doctorId, scheduledAt, durationMinutes);
            _appointments[_count] = a;
            _count++;

            Console.WriteLine("Appointment [" + a.Id + "] created: " + p.FullName + " -> " + d.FullName +
                " at " + scheduledAt.ToString("dd.MM.yyyy HH:mm"));

            return true;
        }

        // find appointment by id, private helper
        private Appointment FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].Id == id)
                    return _appointments[i];
            }

            return null;
        }

        public bool Cancel(int id, string reason = "")
        {
            Appointment a = FindById(id);

            if (a == null)
                return false;

            return a.Cancel(reason);
        }

        public bool Complete(int id)
        {
            Appointment a = FindById(id);

            if (a == null)
                return false;

            return a.Complete();
        }

        // two-pass pattern: count -> allocate -> fill
        public Appointment[] GetByPatient(int patientId)
        {
            int n = 0;
            for (int i = 0; i < _count; i++)
                if (_appointments[i].PatientId == patientId)
                    n++;

            Appointment[] result = new Appointment[n];
            int idx = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].PatientId == patientId)
                {
                    result[idx] = _appointments[i];
                    idx++;
                }
            }

            return result;
        }

        public Appointment[] GetByDoctor(int doctorId)
        {
            int n = 0;
            for (int i = 0; i < _count; i++)
                if (_appointments[i].DoctorId == doctorId)
                    n++;

            Appointment[] result = new Appointment[n];
            int idx = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].DoctorId == doctorId)
                {
                    result[idx] = _appointments[i];
                    idx++;
                }
            }

            return result;
        }

        public Appointment[] GetByDate(DateTime date)
        {
            int n = 0;
            for (int i = 0; i < _count; i++)
                if (_appointments[i].ScheduledAt.Date == date.Date)
                    n++;

            Appointment[] result = new Appointment[n];
            int idx = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].ScheduledAt.Date == date.Date)
                {
                    result[idx] = _appointments[i];
                    idx++;
                }
            }

            return result;
        }

        public Appointment[] GetUpcoming()
        {
            int n = 0;
            for (int i = 0; i < _count; i++)
                if (_appointments[i].IsUpcoming)
                    n++;

            Appointment[] result = new Appointment[n];
            int idx = 0;

            for (int i = 0; i < _count; i++)
            {
                if (_appointments[i].IsUpcoming)
                {
                    result[idx] = _appointments[i];
                    idx++;
                }
            }

            return result;
        }

        // print with names instead of ids
        public void DisplayAppointment(Appointment a)
        {
            Patient p = _patients.FindById(a.PatientId);
            Doctor d = _doctors.FindById(a.DoctorId);

            string patientName = p != null ? p.FullName : "Patient #" + a.PatientId;
            string doctorName = d != null ? d.FullName : "Doctor #" + a.DoctorId;

            string line = "[" + a.Id + "] " + patientName + " -> " + doctorName +
                " | " + a.ScheduledAt.ToString("dd.MM.yyyy HH:mm") + "-" + a.EndsAt.ToString("HH:mm") +
                " | " + a.Status;

            if (a.Notes.Length > 0)
                line += " | " + a.Notes;

            Console.WriteLine(line);
        }

        public void DisplayList(Appointment[] list)
        {
            if (list.Length == 0)
            {
                Console.WriteLine("Not found.");
                return;
            }

            foreach (var a in list)
            {
                DisplayAppointment(a);
            }
        }
    }
}