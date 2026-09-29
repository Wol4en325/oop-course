namespace ClinicApp;

public class DoctorManager
{
    private const int MaxDoctors = 50;
    private Doctor[] _doctors = new Doctor[MaxDoctors];
    private int _count = 0;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public void Add(Doctor doctor)
    {
        if (_count >= MaxDoctors)
        {
            Console.WriteLine("Doctor limit reached.");
            return;
        }

        _doctors[_count] = doctor;
        _count++;

        Console.WriteLine("Doctor [" + doctor.Id + "] " + doctor.FullName + " added.");
    }

    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                return _doctors[i];
            }
        }

        return null;
    }

    public Doctor[] FindBySpeciality(string speciality)
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToLower().Contains(speciality.ToLower()))
            {
                foundCount++;
            }
        }

        Doctor[] result = new Doctor[foundCount];
        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToLower().Contains(speciality.ToLower()))
            {
                result[resultIndex] = _doctors[i];
                resultIndex++;
            }
        }

        return result;
    }

    public Doctor[] GetAll()
    {
        Doctor[] result = new Doctor[_count];

        for (int i = 0; i < _count; i++)
        {
            result[i] = _doctors[i];
        }

        return result;
    }

    public bool Remove(int id)
    {
        int foundIndex = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                foundIndex = i;
                break;
            }
        }

        if (foundIndex == -1)
        {
            return false;
        }

        for (int i = foundIndex; i < _count - 1; i++)
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
            Console.WriteLine("Doctor list is empty.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("=== Doctors (" + _count + " / " + MaxDoctors + ") ===");

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_doctors[i]);
        }
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Doctor list is empty.");
            return;
        }

        int availableCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
            {
                availableCount++;
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Doctor Statistics ===");
        Console.WriteLine("Total: " + _count);
        Console.WriteLine("Available now: " + availableCount);
        Console.WriteLine("By speciality:");

        for (int i = 0; i < _count; i++)
        {
            bool alreadyShown = false;

            for (int j = 0; j < i; j++)
            {
                if (_doctors[j].Speciality.ToLower() == _doctors[i].Speciality.ToLower())
                {
                    alreadyShown = true;
                    break;
                }
            }

            if (!alreadyShown)
            {
                int specialityCount = 0;

                for (int j = 0; j < _count; j++)
                {
                    if (_doctors[j].Speciality.ToLower() == _doctors[i].Speciality.ToLower())
                    {
                        specialityCount++;
                    }
                }

                Console.WriteLine("  " + _doctors[i].Speciality + ": " + specialityCount);
            }
        }
    }
}
