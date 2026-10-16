using System;
using ClinicApp.Managers;

namespace ClinicApp;

public class Clinic
{
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }
    public MedicalRecordManager MedicalRecords { get; }

    public Clinic()
    {
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
        MedicalRecords = new MedicalRecordManager();
    }

    public void Report()
    {
        Console.WriteLine("=== Clinic report ===");
        Console.WriteLine("Patients: " + Patients.Count);
        Console.WriteLine("Doctors: " + Doctors.Count);
        Console.WriteLine("Appointments: " + Appointments.Count);
        Console.WriteLine("Medical records: " + MedicalRecords.Count);
    }
}