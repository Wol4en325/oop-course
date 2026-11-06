using System;
using System.Collections.Generic;
using ClinicApp.Utils;
using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Managers;

public class PatientManager
{
    private List<Patient> _patients;

    public int Count
    {
        get
        {
            return _patients.Count;
        }
    }

    public Patient? this[int index]
    {
        get
        {
            if (index < 0 || index >= _patients.Count)
            {
                return null;
            }

            return _patients[index];
        }
    }

    public PatientManager()
    {
        _patients = new List<Patient>();
    }

    public void Add(Patient patient)
    {
        _patients.Add(patient);
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
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

    public Patient[] FindByName(string name)
    {
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].FullName.Contains(name, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(_patients[i]);
            }
        }

        return result.ToArray();
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                result.Add(_patients[i]);
            }
        }

        return result.ToArray();
    }

    public Patient[] GetAll()
    {
        return _patients.ToArray();
    }
}
