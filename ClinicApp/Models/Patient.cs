using ClinicApp.Utils;
using ClinicApp.Managers;
using ClinicApp.Enums;
namespace ClinicApp.Models;

public class Patient
{
    private static int _nextId = 1;

    private string _firstName = "";
    private string _lastName = "";
    private DateTime _dateOfBirth;
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
    public DateTime DateOfBirth
    {
        get { return _dateOfBirth; }
        set
        {
            if (value.Date > DateTime.Today)
                throw new ArgumentOutOfRangeException(nameof(DateOfBirth), "Date of birth cannot be in the future.");

            if (value.Year < 1900)
                throw new ArgumentOutOfRangeException(nameof(DateOfBirth), "Date of birth cannot be earlier than 1900.");

            _dateOfBirth = value;
        }
    }
    public BloodType BloodType { get; set; }
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
    public string Email { get; set; }

    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }

    public int Age
    {
        get
        {
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;

            if (DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }

    public bool IsAdult
    {
        get
        {
            return Age >= 18;
        }
    }

    public Patient(string firstName, string lastName, DateTime dateOfBirth, BloodType bloodType, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BloodType = bloodType;
        Phone = phone;
        Email = "";
        Id = _nextId++;
    }

    public Patient()
        : this("", "", DateTime.Today, BloodType.Unknown, "")
    {
    }

    public Patient(string firstName, string lastName)
        : this(firstName, lastName, DateTime.Today, BloodType.Unknown, "")
    {
    }

    public string GetAgeCategory()
    {
        if (Age < 18)
        {
            return "child";
        }

        if (Age < 60)
        {
            return "adult";
        }

        return "elderly";
    }

    public override string ToString()
    {
        return "[" + Id + "] " + FullName + " | Age: " + ClinicFormatter.FormatAge(Age) + " (" + GetAgeCategory() + ") | Blood: " + ClinicFormatter.FormatBloodType(BloodType) + " | Phone: " + ClinicFormatter.FormatPhone(Phone);
    }
}
