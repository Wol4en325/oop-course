namespace ClinicApp;

public class Clinic
{
    public string Name { get; set; }
    public string Address { get; set; }

    public PatientManager Patients { get; } = new PatientManager();
    public DoctorManager Doctors { get; } = new DoctorManager();
    public AppointmentManager Appointments { get; } = new AppointmentManager();

    public Clinic(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public Clinic() : this("Default Clinic", "Unknown")
    {
    }
}
