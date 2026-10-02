namespace ClinicApp;

public class Doctor
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Speciality Speciality { get; set; }
    public string LicenseNumber { get; set; }
    public string Phone { get; set; }
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
