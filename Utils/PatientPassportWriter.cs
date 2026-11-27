using System;
using System.IO;
using System.Text;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class PatientPassportWriter
{
    private Clinic _clinic;
    private string _folder;

    public PatientPassportWriter(Clinic clinic, string folder = "patients")
    {
        _clinic = clinic;
        _folder = folder;
        Directory.CreateDirectory(_folder);
    }

    public void OnPatientAdded(object sender, PatientEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnAppointmentCompleted(object sender, AppointmentEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnPlanCompleted(object sender, TreatmentPlanEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    private void WritePassport(int patientId)
    {
        Patient patient = _clinic.Patients.FindById(patientId);

        if (patient == null)
            return;

        string path = Path.Combine(_folder, "passport_" + patientId + ".txt");

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            writer.WriteLine("=== Паспорт пацієнта #" + patient.Id + " ===");
            writer.WriteLine("Згенеровано: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
            writer.WriteLine();

            writer.WriteLine("Ім'я: " + patient.FullName);
            writer.WriteLine("Дата народження: " + patient.DateOfBirth.ToString("dd.MM.yyyy") + " (" + ClinicFormatter.FormatAge(patient.Age) + ")");
            writer.WriteLine("Група крові: " + ClinicFormatter.FormatBloodType(patient.BloodType) + "   Телефон: " + ClinicFormatter.FormatPhone(patient.Phone));
            writer.WriteLine();

            Diagnosis[] diagnoses = _clinic.MedicalRecords.GetDiagnoses(patientId);
            writer.WriteLine("-- Діагнози --");
            for (int i = 0; i < diagnoses.Length; i++)
                writer.WriteLine(diagnoses[i].GetSummary());

            LabResult[] labs = _clinic.MedicalRecords.GetLabResults(patientId);
            writer.WriteLine("-- Аналізи --");
            for (int i = 0; i < labs.Length; i++)
                writer.WriteLine(labs[i].GetSummary());

            Prescription[] prescriptions = _clinic.MedicalRecords.GetPrescriptions(patientId);
            writer.WriteLine("-- Рецепти --");
            for (int i = 0; i < prescriptions.Length; i++)
                writer.WriteLine(prescriptions[i].GetSummary());

            writer.WriteLine();

            Appointment[] appointments = _clinic.Appointments.GetByPatient(patientId);
            writer.WriteLine("-- Записи --");
            for (int i = 0; i < appointments.Length; i++)
                writer.WriteLine(appointments[i]);

            writer.WriteLine();

            TreatmentPlan[] plans = _clinic.TreatmentPlans.GetByPatient(patientId);
            writer.WriteLine("-- Плани лікування --");
            for (int i = 0; i < plans.Length; i++)
                writer.WriteLine("[" + plans[i].Id + "] " + plans[i].Diagnosis + " | " + plans[i].DurationDays + " днів | " + plans[i].Status);

            writer.WriteLine();
            writer.WriteLine("Заборгованість: " + _clinic.Billing.GetPatientDebt(patientId).ToString("F2") + " грн");
        }
    }
}