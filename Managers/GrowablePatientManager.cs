using System;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class GrowablePatientManager
{
    private Patient[] _patients = new Patient[4];
    private int _count = 0;

    public int Count => _count;

    public Patient this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                return null;

            return _patients[index];
        }
    }

    public void Add(Patient p)
    {
        if (_count == _patients.Length)
        {
            Patient[] bigger = new Patient[_patients.Length * 2];

            for (int i = 0; i < _count; i++)
                bigger[i] = _patients[i];

            _patients = bigger;
        }

        _patients[_count] = p;
        _count++;
    }

    public Patient FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
                return _patients[i];
        }

        return null;
    }

    public void ShowAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("No patients yet.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_patients[i]);
        }
    }
}