using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private MedicalRecord[] _records;
    private int _count;

    public MedicalRecordManager(int capacity = 10)
    {
        if (capacity < 1)
        {
            capacity = 10;
        }

        _records = new MedicalRecord[capacity];
        _count = 0;
    }

    public int Count
    {
        get { return _count; }
    }

    public void AddRecord(MedicalRecord record)
    {
        if (record == null)
        {
            throw new ArgumentNullException(nameof(record));
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

    private void Grow()
    {
        MedicalRecord[] newRecords = new MedicalRecord[_records.Length * 2];

        for (int i = 0; i < _count; i++)
        {
            newRecords[i] = _records[i];
        }

        _records = newRecords;
    }
}
