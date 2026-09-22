namespace ClinicApp;

public class Doctor
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Speciality { get; set; }
    public string LicenseNumber { get; set; }
    public string Phone { get; set; }
    public int WorkStartHour { get; set; }
    public int WorkEndHour { get; set; }

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
            return WorkEndHour - WorkStartHour;
        }
    }

    public string WorkSchedule
    {
        get
        {
            return WorkStartHour.ToString("D2") + ":00-" + WorkEndHour.ToString("D2") + ":00";
        }
    }

    public bool IsAvailableNow
    {
        get
        {
            return CanAcceptAt(DateTime.Now.Hour);
        }
    }

    public Doctor()
    {
        Id = _nextId++;
        FirstName = "";
        LastName = "";
        Speciality = "";
        LicenseNumber = "";
        Phone = "";
        WorkStartHour = 8;
        WorkEndHour = 17;
    }

    public Doctor(string firstName, string lastName, string speciality) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
    }

    public Doctor(string firstName, string lastName, string speciality, string licenseNumber, string phone)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        WorkStartHour = 8;
        WorkEndHour = 17;
    }

    public bool CanAcceptAt(int hour)
    {
        return hour >= WorkStartHour && hour < WorkEndHour;
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

        return "[" + Id + "] " + FullName + " | " + Speciality + " | " + LicenseNumber + " | Phone: " + Phone + " | " + WorkSchedule + " (" + WorkingHoursPerDay + " hours) | " + status;
    }
}
