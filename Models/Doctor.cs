using System;
using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Doctor
{
    private static int idCounter = 1;

    private string _fullName = "";

    public int Id { get; }

    public string FullName
    {
        get => _fullName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ім'я не може бути порожнім.", nameof(FullName));
            if (value.Length > 100)
                throw new ArgumentException("Ім'я задовге (максимум 100 символів).", nameof(FullName));

            _fullName = value;
        }
    }

    public Speciality Specialty { get; set; }
    public WorkSchedule Schedule { get; set; }

    public bool IsAvailableNow => Schedule.IsNow;

    public Doctor(string fullName, Speciality specialty, WorkSchedule schedule)
    {
        FullName = fullName;
        Specialty = specialty;
        Schedule = schedule;
        Id = idCounter++;
    }


    public bool IsAvailable(int hour)
    {
        if (hour < 0 || hour > 23)
            return false;

        return Schedule.Contains(hour);
    }

    public override string ToString()
    {
        return "Id=" + Id + ", Name=" + FullName + ", Specialty=" + ClinicFormatter.FormatSpeciality(Specialty) +
            ", Schedule=" + Schedule;
    }
}
