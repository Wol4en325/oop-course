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

    public Doctor? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }
            return _doctors[index];
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

    public bool TryFindById(int id, out Doctor doctor)
    {
        Doctor? found = FindById(id);
        if (found != null)
        {
            doctor = found;
            return true;
        }

        doctor = null!;
        return false;
    }

    public Doctor[] FindBySpeciality(string speciality)
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToString().ToLower().Contains(speciality.ToLower()))
            {
                foundCount++;
            }
        }

        Doctor[] result = new Doctor[foundCount];
        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality.ToString().ToLower().Contains(speciality.ToLower()))
            {
                result[resultIndex] = _doctors[i];
                resultIndex++;
            }
        }

        return result;
    }

    public Doctor[] FindBySpeciality(Speciality speciality)
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality)
            {
                foundCount++;
            }
        }

        Doctor[] result = new Doctor[foundCount];
        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality)
            {
                result[resultIndex] = _doctors[i];
                resultIndex++;
            }
        }

        return result;
    }
}
