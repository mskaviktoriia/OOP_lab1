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

    public Diagnosis[] GetDiagnoses(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis)
                n++;

        Diagnosis[] result = new Diagnosis[n];
        int idx = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis d)
            {
                result[idx] = d;
                idx++;
            }
        }

        return result;
    }

    public LabResult[] GetLabResults(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is LabResult)
                n++;

        LabResult[] result = new LabResult[n];
        int idx = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is LabResult l)
            {
                result[idx] = l;
                idx++;
            }
        }

        return result;
    }

    public Prescription[] GetPrescriptions(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Prescription)
                n++;

        Prescription[] result = new Prescription[n];
        int idx = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Prescription p)
            {
                result[idx] = p;
                idx++;
            }
        }

        return result;
    }

    public Diagnosis[] GetChronicDiagnoses(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis d && d.IsChronic)
                n++;

        Diagnosis[] result = new Diagnosis[n];
        int idx = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis d && d.IsChronic)
            {
                result[idx] = d;
                idx++;
            }
        }

        return result;
    }

    public Prescription[] GetActivePrescriptions(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Prescription p && p.IsActive())
                n++;

        Prescription[] result = new Prescription[n];
        int idx = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Prescription p && p.IsActive())
            {
                result[idx] = p;
                idx++;
            }
        }

        return result;
    }

    public void DisplayPatientSummary(int patientId)
    {
        MedicalRecord[] all = GetByPatient(patientId);

        if (all.Length == 0)
        {
            Console.WriteLine("У пацієнта #" + patientId + " немає записів.");
            return;
        }

        int diagnoses = 0, labs = 0, prescriptions = 0;

        for (int i = 0; i < all.Length; i++)
        {
            if (all[i] is Diagnosis)
                diagnoses++;
            else if (all[i] is LabResult)
                labs++;
            else if (all[i] is Prescription)
                prescriptions++;
        }

        Console.WriteLine("=== Медична картка пацієнта #" + patientId + " ===");
        Console.WriteLine("Всього записів: " + all.Length + " (діагнозів: " + diagnoses + ", аналізів: " + labs + ", рецептів: " + prescriptions + ")");

        Diagnosis[] chronic = GetChronicDiagnoses(patientId);

        if (chronic.Length > 0)
        {
            Console.WriteLine("Хронічні діагнози (" + chronic.Length + "):");

            foreach (var d in chronic)
                Console.WriteLine("  " + d);
        }

        Prescription[] active = GetActivePrescriptions(patientId);

        if (active.Length > 0)
        {
            Console.WriteLine("Активні рецепти (" + active.Length + "):");

            foreach (var p in active)
                Console.WriteLine("  " + p + " | до " + p.ExpiresAt.ToString("dd.MM.yyyy"));
        }
    }
}