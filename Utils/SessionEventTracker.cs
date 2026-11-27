using System;
using System.IO;
using System.Text;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class SessionEventTracker
{
    private Clinic _clinic;

    public int PatientsAdded { get; private set; }
    public int AppointmentsBooked { get; private set; }
    public int UrgentBooked { get; private set; }
    public int AppointmentsCancelled { get; private set; }
    public int AppointmentsCompleted { get; private set; }
    public int PaymentsReceived { get; private set; }
    public int PlansCompleted { get; private set; }

    public SessionEventTracker(Clinic clinic)
    {
        _clinic = clinic;
    }

    public void OnPatientAdded(object sender, PatientEventArgs e)
    {
        PatientsAdded++;
    }

    public void OnAppointmentBooked(object sender, AppointmentEventArgs e)
    {
        AppointmentsBooked++;
    }

    public void OnUrgentAppointmentBooked(object sender, AppointmentEventArgs e)
    {
        UrgentBooked++;
    }

    public void OnAppointmentCancelled(object sender, AppointmentEventArgs e)
    {
        AppointmentsCancelled++;

        if (!_clinic.WaitingRoom.IsEmpty)
        {
            Patient next = _clinic.WaitingRoom.Peek();
            Console.WriteLine("[ЧЕРГА] Слот звільнився. Наступний: " + next.FullName);
        }
    }

    public void OnAppointmentCompleted(object sender, AppointmentEventArgs e)
    {
        AppointmentsCompleted++;
    }

    public void OnPaymentReceived(object sender, PaymentEventArgs e)
    {
        PaymentsReceived++;
    }

    public void OnPlanCompleted(object sender, TreatmentPlanEventArgs e)
    {
        PlansCompleted++;
    }

    private string BuildSummary()
    {
        string nl = Environment.NewLine;
        string text = "=== Підсумок сесії ===" + nl;
        text += "Пацієнтів додано:    " + PatientsAdded + nl;
        text += "Записів створено:    " + AppointmentsBooked + " (термінових: " + UrgentBooked + ")" + nl;
        text += "Скасовано:           " + AppointmentsCancelled + nl;
        text += "Завершено:           " + AppointmentsCompleted + nl;
        text += "Оплат:               " + PaymentsReceived + nl;
        text += "Планів завершено:    " + PlansCompleted + nl;
        return text;
    }

    public void PrintSummary()
    {
        Console.Write(BuildSummary());
    }

    public void SaveSummary(string path = "session_summary.txt")
    {
        string text = BuildSummary() + "Сформовано: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + Environment.NewLine;
        File.WriteAllText(path, text, Encoding.UTF8);
    }
}