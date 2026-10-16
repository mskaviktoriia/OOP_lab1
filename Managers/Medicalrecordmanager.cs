using System;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private const int MaxRecords = 1000;

    private MedicalRecord[] _records = new MedicalRecord[MaxRecords];
    private int _count = 0;

    public int Count => _count;

    public MedicalRecord this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                return null;

            return _records[index];
        }
    }

    public void Add(MedicalRecord record)
    {
        if (_count >= MaxRecords)
        {
            Console.WriteLine("Array is full!");
            return;
        }

        _records[_count] = record;
        _count++;

        Console.WriteLine("Запис [" + record.Id + "] " + record.GetRecordType() + " додано.");
    }

    public MedicalRecord FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].Id == id)
                return _records[i];
        }

        return null;
    }

    public MedicalRecord[] GetByPatient(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId)
                n++;

        MedicalRecord[] result = new MedicalRecord[n];
        int idx = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
            {
                result[idx] = _records[i];
                idx++;
            }
        }

        return result;
    }

    public MedicalRecord[] GetByDoctor(int doctorId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].DoctorId == doctorId)
                n++;

        MedicalRecord[] result = new MedicalRecord[n];
        int idx = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].DoctorId == doctorId)
            {
                result[idx] = _records[i];
                idx++;
            }
        }

        return result;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("No records yet.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_records[i]);
        }
    }

    public void DisplayList(MedicalRecord[] list)
    {
        if (list.Length == 0)
        {
            Console.WriteLine("Not found.");
            return;
        }

        foreach (var r in list)
        {
            Console.WriteLine(r);
        }
    }
}