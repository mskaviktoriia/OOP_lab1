using System;
using System.Globalization;
using System.IO;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class SessionManager
{
    private string _sessionPath;

    public SessionManager(string sessionPath = "session.dat")
    {
        _sessionPath = sessionPath;
    }

    public bool Exists()
    {
        return File.Exists(_sessionPath);
    }

    public void Save(Clinic clinic)
    {
        Patient[] patients = clinic.Patients.GetAll();

        using (StreamWriter writer = new StreamWriter(_sessionPath, false, Encoding.UTF8))
        {
            writer.WriteLine("[PATIENTS]");

            for (int i = 0; i < patients.Length; i++)
            {
                Patient p = patients[i];
                writer.WriteLine(p.FirstName + "," + p.LastName + "," +
                    p.DateOfBirth.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + "," +
                    p.BloodType + "," + p.Phone);
            }

            writer.WriteLine("[END]");
        }
    }

    public int Load(Clinic clinic)
    {
        if (!Exists())
            return 0;

        string[] lines = File.ReadAllLines(_sessionPath, Encoding.UTF8);
        string section = "";
        int added = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (line.Length == 0)
                continue;

            if (line.StartsWith("["))
            {
                section = line;
                continue;
            }

            if (section == "[PATIENTS]")
            {
                try
                {
                    string[] parts = line.Split(',');

                    if (parts.Length != 5)
                        continue;

                    string firstName = parts[0].Trim();
                    string lastName = parts[1].Trim();
                    DateTime dob = DateTime.ParseExact(parts[2].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    BloodType blood = (BloodType)Enum.Parse(typeof(BloodType), parts[3].Trim());
                    string phone = parts[4].Trim();

                    if (PatientExists(clinic, firstName, lastName, dob))
                        continue;

                    Patient p = new Patient(firstName, lastName, dob, phone, "Not diagnosed", blood);
                    clinic.Patients.Add(p);
                    added++;
                }
                catch (Exception)
                {
                }
            }
        }

        return added;
    }

    private bool PatientExists(Clinic clinic, string firstName, string lastName, DateTime dob)
    {
        Patient[] patients = clinic.Patients.GetAll();

        for (int i = 0; i < patients.Length; i++)
        {
            if (patients[i].FirstName == firstName && patients[i].LastName == lastName && patients[i].DateOfBirth.Date == dob.Date)
                return true;
        }

        return false;
    }
}