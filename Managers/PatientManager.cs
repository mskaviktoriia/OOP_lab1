using System;
using System.Collections.Generic;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class PatientManager
{
    private List<Patient> _patients = new List<Patient>();

    public int Count => _patients.Count;

    public Patient this[int index]
    {
        get
        {
            if (index < 0 || index >= _patients.Count)
                return null;

            return _patients[index];
        }
    }

    public void Add(Patient p)
    {
        _patients.Add(p);
    }

    public Patient[] GetAll()
    {
        return _patients.ToArray();
    }

    public void ShowAll()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine("No patients yet.");
            return;
        }

        for (int i = 0; i < _patients.Count; i++)
        {
            Console.WriteLine(_patients[i]);
        }
    }

    public Patient FindByName(string name)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].FullName.ToLower().Contains(name.ToLower()))
                return _patients[i];
        }

        return null;
    }

    public bool Remove(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                _patients.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public bool TryFindById(int id, out Patient patient)
    {
        patient = FindById(id);
        return patient != null;
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].BloodType == bloodType)
                result.Add(_patients[i]);
        }

        return result.ToArray();
    }

    public void Statistics()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine("No patients yet.");
            return;
        }

        int children = 0, adults = 0, seniors = 0;
        double totalAge = 0;

        for (int i = 0; i < _patients.Count; i++)
        {
            totalAge += _patients[i].Age;

            string cat = _patients[i].Classify();
            if (cat == "Child") children++;
            else if (cat == "Adult") adults++;
            else seniors++;
        }

        Console.WriteLine("Total patients: " + _patients.Count);
        Console.WriteLine("Average age: " + (totalAge / _patients.Count).ToString("F1"));
        Console.WriteLine("Children: " + children);
        Console.WriteLine("Adults: " + adults);
        Console.WriteLine("Seniors: " + seniors);
    }

    public Patient FindById(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
                return _patients[i];
        }

        return null;
    }
}