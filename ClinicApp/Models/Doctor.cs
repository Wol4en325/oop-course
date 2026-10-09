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
        set { _firstName = value; }
    }
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }
    public Speciality Speciality { get; set; }
    public string LicenseNumber
    {
        get { return _licenseNumber; }
        set { _licenseNumber = value; }
    }
    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
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
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = new WorkSchedule(8, 17);
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
