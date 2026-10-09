using System;
using System.Collections.Generic;
using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Managers;

public class DoctorManager
{
    private Doctor[] doctors = new Doctor[100];
    private int count = 0;
    public Doctor this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                return null;

            return doctors[index];
        }
    }
    public void Add(Doctor d)
    {
        if (count >= doctors.Length)
        {
            Console.WriteLine("Array is full!");
            return;
        }

        doctors[count] = d;
        count++;
    }

    public void ShowAll()
    {
        if (count == 0)
        {
            Console.WriteLine("No doctors yet.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(doctors[i]);
        }
    }

    public Doctor FindBySpeciality(string query)
    {
        for (int i = 0; i < count; i++)
        {
            if (ClinicFormatter.FormatSpeciality(doctors[i].Specialty).ToLower().Contains(query.ToLower()))
                return doctors[i];
        }

        return null;
    }
    public Doctor FindBySpeciality(Speciality speciality)
    {
        for (int i = 0; i < count; i++)
        {
            if (doctors[i].Specialty == speciality)
                return doctors[i];
        }

        return null;
    }

    public bool TryFindById(int id, out Doctor doctor)
    {
        doctor = FindById(id);
        return doctor != null;
    }

    public bool Remove(int id)
    {
        int index = -1;

        for (int i = 0; i < count; i++)
        {
            if (doctors[i].Id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return false;

        for (int i = index; i < count - 1; i++)
        {
            doctors[i] = doctors[i + 1];
        }

        doctors[count - 1] = null;
        count--;

        return true;
    }

    public Doctor FindById(int id)
    {
        for (int i = 0; i < count; i++)
        {
            if (doctors[i].Id == id)
                return doctors[i];
        }

        return null;
    }

    public void Statistics()
    {
        if (count == 0)
        {
            Console.WriteLine("No doctors yet.");
            return;
        }

        List<string> uniqueSpecs = new List<string>();

        for (int i = 0; i < count; i++)
        {
            string spec = doctors[i].Specialty.ToString();

            if (!uniqueSpecs.Contains(spec))
                uniqueSpecs.Add(spec);
        }

        Console.WriteLine("Total doctors: " + count);
        Console.WriteLine("Unique specialities: " + uniqueSpecs.Count);

        foreach (var s in uniqueSpecs)
        {
            Console.WriteLine("- " + s);
        }
    }

    public int Count => count;
}
