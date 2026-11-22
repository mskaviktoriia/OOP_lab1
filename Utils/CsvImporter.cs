using System;
using System.Globalization;
using System.IO;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public static class CsvImporter
{
    public static ImportResult ImportPatients(Clinic clinic, string filePath)
    {
        ImportResult result = new ImportResult();

        if (!File.Exists(filePath))
        {
            result.AddError(0, "Файл не знайдено: " + filePath);
            return result;
        }

        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        for (int i = 1; i < lines.Length; i++)
        {
            int lineNumber = i + 1;
            string line = lines[i];

            if (line.Trim().Length == 0)
                continue;

            string[] parts = line.Split(',');

            if (parts.Length != 5)
            {
                result.AddError(lineNumber, "Рядок має " + parts.Length + " " + FieldsWord(parts.Length) + " замість 5.");
                continue;
            }

            string firstName = parts[0].Trim();
            string lastName = parts[1].Trim();
            string phone = parts[4].Trim();

            DateTime dob;
            if (!DateTime.TryParseExact(parts[2].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
            {
                result.AddError(lineNumber, "Некоректна дата народження.");
                continue;
            }

            BloodType blood = BloodType.Unknown;
            string bloodText = parts[3].Trim();

            if (bloodText.Length > 0)
            {
                try
                {
                    blood = (BloodType)Enum.Parse(typeof(BloodType), bloodText);
                }
                catch (ArgumentException)
                {
                    result.AddError(lineNumber, "Невідома група крові.");
                    continue;
                }

                if (!Enum.IsDefined(typeof(BloodType), blood))
                {
                    result.AddError(lineNumber, "Невідома група крові.");
                    continue;
                }
            }

            try
            {
                Patient p = new Patient(firstName, lastName, dob, phone, "Not diagnosed", blood);
                clinic.Patients.Add(p);
                result.AddSuccess();
            }
            catch (ArgumentException e)
            {
                result.AddError(lineNumber, FirstLine(e.Message));
            }
        }

        return result;
    }

    private static string FirstLine(string message)
    {
        int index = message.IndexOf('\n');

        if (index >= 0)
            return message.Substring(0, index).Trim();

        return message.Trim();
    }

    private static string FieldsWord(int count)
    {
        if (count == 1)
            return "поле";

        if (count >= 2 && count <= 4)
            return "поля";

        return "полів";
    }
}