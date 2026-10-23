using System;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Doctor : ISchedulable
{
    private static int idCounter = 1;

    private string _firstName = "";
    private string _lastName = "";
    private string _licenseNumber = "";
    private string _phone = "";

    public int Id { get; }

    public string FirstName
    {
        get => _firstName;
        set
        {
            ClinicValidator.ValidateName(value, nameof(FirstName));
            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            ClinicValidator.ValidateName(value, nameof(LastName));
            _lastName = value;
        }
    }

    public string LicenseNumber
    {
        get => _licenseNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер ліцензії не може бути порожнім.", nameof(LicenseNumber));

            _licenseNumber = value;
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            ClinicValidator.ValidatePhone(value);
            _phone = value;
        }
    }

    public Speciality Specialty { get; set; }
    public WorkSchedule Schedule { get; set; }

    public string FullName => FirstName + " " + LastName;
    public bool IsAvailableNow => Schedule.IsNow;

    public Doctor(string firstName, string lastName, string licenseNumber, string phone, Speciality specialty, WorkSchedule schedule)
    {
        FirstName = firstName;
        LastName = lastName;
        LicenseNumber = licenseNumber;
        Phone = phone;
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

    public bool CanSchedule(DateTime at)
    {
        return IsAvailable(at.Hour);
    }

    public DateTime[] GetAvailableSlots(DateTime date, int slotCount)
    {
        ClinicValidator.ValidatePositive(slotCount, nameof(slotCount));

        int n = slotCount < Schedule.HoursPerDay ? slotCount : Schedule.HoursPerDay;
        DateTime[] slots = new DateTime[n];

        for (int i = 0; i < n; i++)
            slots[i] = date.Date.AddHours(Schedule.Start + i);

        return slots;
    }

    public override string ToString()
    {
        return "Id=" + Id + ", Name=" + FullName + ", License=" + LicenseNumber + ", Phone=" + ClinicFormatter.FormatPhone(Phone) +
            ", Specialty=" + ClinicFormatter.FormatSpeciality(Specialty) + ", Schedule=" + Schedule;
    }
}