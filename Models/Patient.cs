using System;
using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Patient
{
    private static int idCounter = 1;

    private string _firstName = "";
    private string _lastName = "";
    private DateTime _dateOfBirth;
    private string _phone = "";
    private string _diagnosis = "";
    private string _email = "";

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

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            ClinicValidator.ValidateDate(value, nameof(DateOfBirth));
            _dateOfBirth = value;
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

    public string Diagnosis
    {
        get => _diagnosis;
        set => _diagnosis = value;
    }

    public string Email
    {
        get => _email;
        set
        {
            if (!string.IsNullOrEmpty(value))
                ClinicValidator.ValidateEmail(value);

            _email = value ?? "";
        }
    }

    public BloodType BloodType { get; set; }
    public DateTime AdmissionDate { get; }

    public string FullName => FirstName + " " + LastName;

    public int Age
    {
        get
        {
            int age = DateTime.Today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
    }

    public bool IsAdult => Age >= 18;
    public int DaysInClinic => (DateTime.Now - AdmissionDate).Days;
    public string ShortInfo => $"#{Id} {FullName} ({Age} y.o.)";

    public Patient(string firstName, string lastName, DateTime dateOfBirth, string phone, string diagnosis = "Not diagnosed", BloodType bloodType = BloodType.Unknown)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Phone = phone;
        Diagnosis = diagnosis;
        BloodType = bloodType;
        AdmissionDate = DateTime.Now;

        Id = idCounter++;
    }


    public string Classify()
    {
        if (Age < 18)
            return "Child";
        if (Age < 65)
            return "Adult";
        return "Senior";
    }

    public override string ToString()
    {
        return "Id=" + Id + ", Name=" + FullName + ", Age=" + ClinicFormatter.FormatAge(Age) + ", Phone=" + ClinicFormatter.FormatPhone(Phone) +
            ", Diagnosis=" + Diagnosis + ", BloodType=" + ClinicFormatter.FormatBloodType(BloodType) + ", IsAdult=" + IsAdult +
            ", DaysInClinic=" + DaysInClinic + ", Category=" + Classify() + ", AdmissionDate=" + AdmissionDate;
    }
}