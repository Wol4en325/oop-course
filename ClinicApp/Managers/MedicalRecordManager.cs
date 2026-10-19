using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private const int MaxRecords = 1000;
    private MedicalRecord[] _records;
    private int _count;

    public MedicalRecordManager(int capacity = 10)
    {
        if (capacity < 1)
        {
            capacity = 10;
        }

        if (capacity > MaxRecords)
        {
            capacity = MaxRecords;
        }

        _records = new MedicalRecord[capacity];
        _count = 0;
    }

    public int Count
    {
        get { return _count; }
    }

    public MedicalRecord this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _records[index];
        }
    }

    public void AddRecord(MedicalRecord record)
    {
        if (record == null)
        {
            throw new ArgumentNullException(nameof(record));
        }

        if (_count >= MaxRecords)
        {
            throw new InvalidOperationException("The maximum number of medical records is 1000.");
        }

        if (_count == _records.Length)
        {
            Grow();
        }

        _records[_count] = record;
        _count++;
    }

    public MedicalRecord? GetRecordById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].Id == id)
            {
                return _records[i];
            }
        }

        return null;
    }

    public void DisplayAllRecords()
    {
        if (_count == 0)
        {
            Console.WriteLine("No medical records found.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_records[i]);
        }
    }

    public void DisplayRecordsByType(string recordType)
    {
        bool found = false;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].GetRecordType().Equals(
                recordType,
                StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(_records[i]);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No records of this type found.");
        }
    }

    public int CountActiveRecords()
    {
        int activeCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].IsActive())
            {
                activeCount++;
            }
        }

        return activeCount;
    }

    public int CountRecordsByType(string recordType)
    {
        int typeCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].GetRecordType().Equals(
                recordType,
                StringComparison.OrdinalIgnoreCase))
            {
                typeCount++;
            }
        }

        return typeCount;
    }

    public List<Diagnosis> GetDiagnoses(int patientId)
    {
        List<Diagnosis> result = new List<Diagnosis>();

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis diagnosis)
            {
                result.Add(diagnosis);
            }
        }

        return result;
    }

    public List<LabResult> GetLabResults(int patientId)
    {
        List<LabResult> result = new List<LabResult>();

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is LabResult labResult)
            {
                result.Add(labResult);
            }
        }

        return result;
    }

    public List<Prescription> GetPrescriptions(int patientId)
    {
        List<Prescription> result = new List<Prescription>();

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId && _records[i] is Prescription prescription)
            {
                result.Add(prescription);
            }
        }

        return result;
    }

    public List<Diagnosis> GetChronicDiagnoses(int patientId)
    {
        List<Diagnosis> result = new List<Diagnosis>();

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Diagnosis diagnosis &&
                diagnosis.IsChronic)
            {
                result.Add(diagnosis);
            }
        }

        return result;
    }

    public List<Prescription> GetActivePrescriptions(int patientId)
    {
        List<Prescription> result = new List<Prescription>();

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Prescription prescription &&
                prescription.IsActive())
            {
                result.Add(prescription);
            }
        }

        return result;
    }

    public void DisplayPatientSummary(int patientId)
    {
        Console.WriteLine("Medical record for patient ID: " + patientId);

        Console.WriteLine("\nDiagnoses:");
        DisplayList(GetDiagnoses(patientId));

        Console.WriteLine("\nLab results:");
        DisplayList(GetLabResults(patientId));

        Console.WriteLine("\nPrescriptions:");
        DisplayList(GetPrescriptions(patientId));

        Console.WriteLine("\nChronic diagnoses: " + GetChronicDiagnoses(patientId).Count);
        Console.WriteLine("Active prescriptions: " + GetActivePrescriptions(patientId).Count);
    }

    private void DisplayList<T>(List<T> records)
    {
        if (records.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        foreach (T record in records)
        {
            Console.WriteLine(record);
        }
    }

    private void Grow()
    {
        if (_records.Length >= MaxRecords)
        {
            throw new InvalidOperationException("The maximum number of medical records is 1000.");
        }

        int newCapacity = _records.Length * 2;

        if (newCapacity > MaxRecords)
        {
            newCapacity = MaxRecords;
        }

        MedicalRecord[] newRecords = new MedicalRecord[newCapacity];

        for (int i = 0; i < _count; i++)
        {
            newRecords[i] = _records[i];
        }

        _records = newRecords;
    }
}
