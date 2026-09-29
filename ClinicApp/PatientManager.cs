namespace ClinicApp;

public class PatientManager
{
    private const int MaxPatients = 100;
    private Patient[] _patients = new Patient[MaxPatients];
    private int _count = 0;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public void Add(Patient patient)
    {
        if (_count >= MaxPatients)
        {
            Console.WriteLine("Patient limit reached.");
            return;
        }

        _patients[_count] = patient;
        _count++;

        Console.WriteLine("Patient [" + patient.Id + "] " + patient.FullName + " added.");
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

    public Patient[] FindByName(string name)
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            string firstName = _patients[i].FirstName.ToLower();
            string lastName = _patients[i].LastName.ToLower();
            string searchName = name.ToLower();

            if (firstName.Contains(searchName) || lastName.Contains(searchName))
            {
                foundCount++;
            }
        }

        Patient[] result = new Patient[foundCount];
        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            string firstName = _patients[i].FirstName.ToLower();
            string lastName = _patients[i].LastName.ToLower();
            string searchName = name.ToLower();

            if (firstName.Contains(searchName) || lastName.Contains(searchName))
            {
                result[resultIndex] = _patients[i];
                resultIndex++;
            }
        }

        return result;
    }

    public bool Remove(int id)
    {
        int foundIndex = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
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
            Console.WriteLine("Patient list is empty.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("=== Patients (" + _count + " / " + MaxPatients + ") ===");

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_patients[i]);
        }
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Patient list is empty.");
            return;
        }

        int totalAge = 0;
        int youngestIndex = 0;
        int oldestIndex = 0;
        int adultCount = 0;

        for (int i = 0; i < _count; i++)
        {
            totalAge += _patients[i].Age;

            if (_patients[i].Age < _patients[youngestIndex].Age)
            {
                youngestIndex = i;
            }

            if (_patients[i].Age > _patients[oldestIndex].Age)
            {
                oldestIndex = i;
            }

            if (_patients[i].IsAdult)
            {
                adultCount++;
            }
        }

        double averageAge = (double)totalAge / _count;

        Console.WriteLine();
        Console.WriteLine("=== Patient Statistics ===");
        Console.WriteLine("Total: " + _count);
        Console.WriteLine("Average age: " + averageAge.ToString("F1"));
        Console.WriteLine("Youngest: " + _patients[youngestIndex].FullName + " (" + _patients[youngestIndex].Age + " years)");
        Console.WriteLine("Oldest: " + _patients[oldestIndex].FullName + " (" + _patients[oldestIndex].Age + " years)");
        Console.WriteLine("Adults: " + adultCount + " of " + _count);
    }
}
