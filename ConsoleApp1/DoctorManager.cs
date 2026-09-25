namespace ClinicApp;

public class DoctorManager
{
    private const int MaxDoctors = 50;

    private Doctor[] _doctors = new Doctor[MaxDoctors];
    private int _count = 0;

    public int Count => _count;

    public void Add(Doctor doctor)
    {
        if (_count >= MaxDoctors)
        {
            Console.WriteLine("Ліміт лікарів вичерпано.");
            return;
        }

        _doctors[_count] = doctor;
        _count++;

        Console.WriteLine($"Лікаря [{doctor.Id}] {doctor.FullName} додано.");
    }

    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
                return _doctors[i];
        }
        return null;
    }

    public Doctor[] FindBySpeciality(string speciality)
    {
        string search = speciality.ToLower();
        int matches = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToLower().Contains(search))
                matches++;
        }

        Doctor[] result = new Doctor[matches];
        int j = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToLower().Contains(search))
            {
                result[j] = _doctors[i];
                j++;
            }
        }

        return result;
    }

    public Doctor[] GetAll()
    {
        Doctor[] copy = new Doctor[_count];
        Array.Copy(_doctors, copy, _count);
        return copy;
    }

    public bool Remove(int id)
    {
        int index = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return false;

        for (int i = index; i < _count - 1; i++)
        {
            _doctors[i] = _doctors[i + 1];
        }

        _doctors[_count - 1] = null!;
        _count--;

        return true;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Список лікарів порожній.");
            return;
        }

        Console.WriteLine($"=== Лікарі ({_count} / {MaxDoctors}) ===");
        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_doctors[i]);
        }
        Console.WriteLine("────────────────────────────────────────────────────────────");
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Статистика недоступна - список порожній.");
            return;
        }

        int availableNow = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
                availableNow++;
        }

        Console.WriteLine("=== Статистика лікарів ===");
        Console.WriteLine("Всього:         " + _count);
        Console.WriteLine("Доступні зараз: " + availableNow);
        Console.WriteLine("По спеціальностях:");

        for (int i = 0; i < _count; i++)
        {
            bool seenBefore = false;

            for (int j = 0; j < i; j++)
            {
                if (_doctors[j].Speciality.ToLower() == _doctors[i].Speciality.ToLower())
                {
                    seenBefore = true;
                    break;
                }
            }

            if (seenBefore)
                continue;

            int specCount = 0;
            for (int k = 0; k < _count; k++)
            {
                if (_doctors[k].Speciality.ToLower() == _doctors[i].Speciality.ToLower())
                    specCount++;
            }

            Console.WriteLine($"  {_doctors[i].Speciality}: {specCount}");
        }

        Console.WriteLine("==========================");
    }
}