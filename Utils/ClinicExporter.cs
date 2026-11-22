using System;
using System.IO;
using System.Text;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private Clinic _clinic;
    private string _baseDir;

    public ClinicExporter(Clinic clinic, string baseDir = "reports")
    {
        _clinic = clinic;
        _baseDir = baseDir;
    }

    private string GetDateDir()
    {
        string dir = Path.Combine(_baseDir, DateTime.Now.ToString("yyyy-MM-dd"));
        Directory.CreateDirectory(dir);
        return dir;
    }

    private void WriteHeader(StreamWriter writer, string title)
    {
        writer.WriteLine("=== " + title + " — звіт ===");
        writer.WriteLine("Згенеровано: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
        writer.WriteLine(new string('-', 40));
    }

    private string Number(int i)
    {
        return (i + 1).ToString().PadLeft(3) + ". ";
    }

    public string ExportPatients()
    {
        string path = Path.Combine(GetDateDir(), "patients.txt");
        Patient[] patients = _clinic.Patients.GetAll();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            WriteHeader(writer, "Пацієнти");

            for (int i = 0; i < patients.Length; i++)
            {
                Patient p = patients[i];
                writer.WriteLine(Number(i) + "[" + p.Id + "] " + p.FullName + " | " + ClinicFormatter.FormatAge(p.Age) +
                    " | " + ClinicFormatter.FormatBloodType(p.BloodType) + " | " + ClinicFormatter.FormatPhone(p.Phone));
            }

            writer.WriteLine(new string('-', 40));
            writer.WriteLine("Всього: " + patients.Length);
        }

        return path;
    }

    public string ExportAppointments()
    {
        string path = Path.Combine(GetDateDir(), "appointments.txt");
        Appointment[] appointments = _clinic.Appointments.GetAll();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            WriteHeader(writer, "Записи на прийом");

            for (int i = 0; i < appointments.Length; i++)
                writer.WriteLine(Number(i) + appointments[i]);

            writer.WriteLine(new string('-', 40));
            writer.WriteLine("Всього: " + appointments.Length);
        }

        return path;
    }

    public string ExportBilling()
    {
        string path = Path.Combine(GetDateDir(), "billing.txt");
        IPayable[] unpaid = _clinic.Billing.GetAllUnpaid();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            WriteHeader(writer, "Рахунки");

            for (int i = 0; i < unpaid.Length; i++)
                writer.WriteLine(Number(i) + unpaid[i] + " | Сума: " + unpaid[i].GetCost().ToString("F2") + " грн");

            writer.WriteLine(new string('-', 40));
            writer.WriteLine("Неоплачених: " + unpaid.Length);
            writer.WriteLine("Загальний борг: " + _clinic.Billing.GetTotalDebt().ToString("F2") + " грн");
        }

        return path;
    }

    public string ExportTreatmentPlans()
    {
        string path = Path.Combine(GetDateDir(), "treatment_plans.txt");
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetAll();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            WriteHeader(writer, "Плани лікування");

            for (int i = 0; i < plans.Length; i++)
                writer.WriteLine(Number(i) + plans[i]);

            writer.WriteLine(new string('-', 40));
            writer.WriteLine("Всього: " + plans.Length);
        }

        return path;
    }

    public string[] ExportAll()
    {
        string[] paths = new string[4];

        paths[0] = ExportPatients();
        paths[1] = ExportAppointments();
        paths[2] = ExportBilling();
        paths[3] = ExportTreatmentPlans();

        return paths;
    }
}