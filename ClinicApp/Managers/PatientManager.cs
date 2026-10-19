using ClinicApp.Utils;
using ClinicApp.Models;
using ClinicApp.Enums;
namespace ClinicApp.Managers;

public class PatientManager
{
    private const int InitialCapacity = 4;
    private Patient[] _patients;
    private int _count;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public Patient? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }
            return _patients[index];
        }
    }

    public PatientManager()
    {
        _patients = new Patient[InitialCapacity];
        _count = 0;
    }

    public void Add(Patient patient)
    {
        if (_count == _patients.Length)
        {
            Resize();
        }

        _patients[_count] = patient;
        _count++;
    }

    private void Resize()
    {
        Patient[] newArray = new Patient[_patients.Length * 2];
        for (int i = 0; i < _count; i++)
        {
            newArray[i] = _patients[i];
        }
        _patients = newArray;
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }
        return null;
    }

    public bool TryFindById(int id, out Patient patient)
    {
        Patient? found = FindById(id);
        if (found != null)
        {
            patient = found;
            return true;
        }

        patient = null!;
        return false;
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        int foundCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                foundCount++;
            }
        }

        Patient[] result = new Patient[foundCount];
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                result[idx++] = _patients[i];
            }
        }

        return result;
    }
}



