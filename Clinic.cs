using System;
using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp;

public class Clinic
{
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }
    public MedicalRecordManager MedicalRecords { get; }
    public BillingManager Billing { get; }
    public WaitingQueue<Patient> WaitingRoom { get; }
    public AnalyticsManager Analytics { get; }
    public TreatmentPlanManager TreatmentPlans { get; }
    public ClinicLogger Logger { get; }
    public ClinicExporter Exporter { get; }
    public SessionManager Session { get; }
    public PatientPassportWriter Passport { get; }
    public SessionEventTracker Tracker { get; }

    public Clinic()
    {
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
        MedicalRecords = new MedicalRecordManager();
        Billing = new BillingManager(Appointments);
        WaitingRoom = new WaitingQueue<Patient>();
        Analytics = new AnalyticsManager(Appointments, Doctors, Patients);
        TreatmentPlans = new TreatmentPlanManager();
        Logger = new ClinicLogger();
        Exporter = new ClinicExporter(this);
        Session = new SessionManager();
        Passport = new PatientPassportWriter(this);
        Tracker = new SessionEventTracker(this);

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        Patients.PatientAdded += Logger.OnPatientAdded;
        Patients.PatientAdded += Passport.OnPatientAdded;
        Patients.PatientAdded += Tracker.OnPatientAdded;
        Appointments.AppointmentBooked += Logger.OnAppointmentBooked;
        Appointments.AppointmentBooked += Tracker.OnAppointmentBooked;
        Appointments.UrgentAppointmentBooked += Logger.OnUrgentAppointmentBooked;
        Appointments.UrgentAppointmentBooked += Tracker.OnUrgentAppointmentBooked;
        Appointments.AppointmentCancelled += Logger.OnAppointmentCancelled;
        Appointments.AppointmentCancelled += Tracker.OnAppointmentCancelled;
        Appointments.AppointmentCompleted += Logger.OnAppointmentCompleted;
        Appointments.AppointmentCompleted += Passport.OnAppointmentCompleted;
        Appointments.AppointmentCompleted += Tracker.OnAppointmentCompleted;
        Billing.PaymentReceived += Logger.OnPaymentReceived;
        Billing.PaymentReceived += Tracker.OnPaymentReceived;
        TreatmentPlans.PlanCompleted += Logger.OnPlanCompleted;
        TreatmentPlans.PlanCompleted += Passport.OnPlanCompleted;
        TreatmentPlans.PlanCompleted += Tracker.OnPlanCompleted;
    }

    public void Report()
    {
        Console.WriteLine("=== Clinic report ===");
        Console.WriteLine("Patients: " + Patients.Count);
        Console.WriteLine("Doctors: " + Doctors.Count);
        Console.WriteLine("Appointments: " + Appointments.Count);
        Console.WriteLine("Medical records: " + MedicalRecords.Count);
        Console.WriteLine("Total debt: " + Billing.GetTotalDebt().ToString("F2") + " грн");
    }
}