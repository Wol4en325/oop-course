namespace ClinicApp;

public class Patient
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string BloodType { get; set; }
    public string Phone { get; set; }
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

    public Patient()
    {
        Id = _nextId++;
        FirstName = "";
        LastName = "";
        DateOfBirth = DateTime.Today;
        BloodType = "";
        Phone = "";
        Email = "";
    }

    public Patient(string firstName, string lastName) : this()
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public Patient(string firstName, string lastName, DateTime dateOfBirth, string bloodType, string phone)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BloodType = bloodType;
        Phone = phone;
        Email = "";
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
        return "[" + Id + "] " + FullName + " | Age: " + Age + " (" + GetAgeCategory() + ") | Blood: " + BloodType + " | Phone: " + Phone;
    }
}
