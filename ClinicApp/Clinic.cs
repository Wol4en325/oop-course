using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp;

public class Clinic
{
    public string Name { get; set; }
    public string Address { get; set; }

    public PatientManager Patients { get; } = new PatientManager();
    public DoctorManager Doctors { get; } = new DoctorManager();
    public AppointmentManager Appointments { get; } = new AppointmentManager();
    public MedicalRecordManager MedicalRecords { get; } = new MedicalRecordManager();
    public BillingManager Billing { get; }
    public WaitingQueue<Patient> WaitingRoom { get; } = new WaitingQueue<Patient>();
    public AnalyticsManager Analytics { get; }
    public TreatmentPlanManager TreatmentPlans { get; } = new TreatmentPlanManager();
    public ClinicLogger Logger { get; }

    public Clinic(string name, string address)
    {
        Name = name;
        Address = address;
        Logger = new ClinicLogger();
        Billing = new BillingManager(Appointments);
        Analytics = new AnalyticsManager(Appointments, Doctors, Patients);
    }

    public Clinic() : this("Default Clinic", "Unknown")
    {
    }
}
