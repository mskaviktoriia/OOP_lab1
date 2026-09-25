namespace ClinicApp;

public class GrowablePatientManager
{
    private Patient[] _patients = new Patient[4];
    private int _count = 0;

    public int Count => _count;

    public int Capacity => _patients.Length;

    public void Add(Patient patient)
    {
        if (_count == _patients.Length)
        {
            Grow();
        }

        _patients[_count] = patient;
        _count++;
    }

    private void Grow()
    {
        int oldLength = _patients.Length;
        int newLength = oldLength * 2;

        Patient[] newArray = new Patient[newLength];

        for (int i = 0; i < _count; i++)
        {
            newArray[i] = _patients[i];
        }

        _patients = newArray;

        Console.WriteLine($"  Масив заповнений! Розширення: {oldLength} → {newLength}");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
                return _patients[i];
        }
        return null;
    }

    public bool Remove(int id)
    {
        int index = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return false;

        for (int i = index; i < _count - 1; i++)
        {
            _patients[i] = _patients[i + 1];
        }

        _patients[_count - 1] = null!;
        _count--;

        return true;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Список пацієнтів порожній.");
            return;
        }

        Console.WriteLine($"=== Пацієнти (ємність: {Capacity}) ===");
        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_patients[i]);
        }
    }
}