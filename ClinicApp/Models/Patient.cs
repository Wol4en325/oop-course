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
    private string _email = "";

    public int Id { get; }
    public string FirstName
    {
        get { return _firstName; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(FirstName));

            _firstName = value;
        }
    }
    public string LastName
    {
        get { return _lastName; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(LastName));

            _lastName = value;
        }
    }
    public DateTime DateOfBirth
    {
        get { return _dateOfBirth; }
        set
        {
            ClinicValidator.ValidateDate(value, nameof(DateOfBirth));

            _dateOfBirth = value;
        }
    }
    public BloodType BloodType { get; set; }
    public string Phone
    {
        get { return _phone; }
        set
        {
            ClinicValidator.ValidatePhone(value);

            _phone = value;
        }
    }
    public string Email
    {
        get { return _email; }
        set
        {
            ClinicValidator.ValidateEmail(value);
            _email = value;
        }
    }

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
