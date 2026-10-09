using ClinicApp.Utils;
using ClinicApp.Managers;
using ClinicApp.Enums;
namespace ClinicApp.Models;

public class Doctor
{
    private static int _nextId = 1;

    private string _firstName = "";
    private string _lastName = "";
    private string _licenseNumber = "";
    private string _phone = "";

    public int Id { get; }
    public string FirstName
    {
        get { return _firstName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                throw new ArgumentException("First name must contain 1 to 50 characters.", nameof(FirstName));

            _firstName = value;
        }
    }
    public string LastName
    {
        get { return _lastName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                throw new ArgumentException("Last name must contain 1 to 50 characters.", nameof(LastName));

            _lastName = value;
        }
    }
    public Speciality Speciality { get; set; }
    public string LicenseNumber
    {
        get { return _licenseNumber; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("License number cannot be empty.", nameof(LicenseNumber));

            _licenseNumber = value;
        }
    }
    public string Phone
    {
        get { return _phone; }
        set
        {
            if (value == null || value.Length != 10)
                throw new ArgumentException("Phone must contain exactly 10 digits.", nameof(Phone));

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] < '0' || value[i] > '9')
                    throw new ArgumentException("Phone must contain only digits.", nameof(Phone));
            }

            _phone = value;
        }
    }
    public WorkSchedule Schedule { get; set; }

    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }

    public int WorkingHoursPerDay
    {
        get
        {
            return Schedule.HoursPerDay;
        }
    }

    public string WorkSchedule
    {
        get
        {
            return Schedule.Display;
        }
    }

    public bool IsAvailableNow
    {
        get
        {
            return Schedule.IsNow;
        }
    }

    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = new WorkSchedule(8, 17);
        Id = _nextId++;
    }

    public Doctor()
        : this("", "", Speciality.General, "", "")
    {
    }

    public Doctor(string firstName, string lastName, Speciality speciality)
        : this(firstName, lastName, speciality, "", "")
    {
    }

    public bool CanAcceptAt(int hour)
    {
        return Schedule.Contains(hour);
    }

    public override string ToString()
    {
        string status;

        if (IsAvailableNow)
        {
            status = "available now";
        }
        else
        {
            status = "outside working hours";
        }

        return "[" + Id + "] " + FullName + " | " + ClinicFormatter.FormatSpeciality(Speciality) + " | " + LicenseNumber + " | Phone: " + ClinicFormatter.FormatPhone(Phone) + " | " + Schedule + " | " + status;
    }
}
