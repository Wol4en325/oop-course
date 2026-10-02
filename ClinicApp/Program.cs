namespace ClinicApp;

class Program
{
    static void Main(string[] args)
    {
        Clinic clinic = new Clinic("City Health Clinic", "Main St. 12");

        Patient patient1 = new Patient("John", "Doe", new DateTime(1990, 5, 15), BloodType.APositive, "0501234567");
        Patient patient2 = new Patient("Anna", "Smith", new DateTime(2010, 8, 20), BloodType.ONegative, "0679876543");

        clinic.Patients.Add(patient1);
        clinic.Patients.Add(patient2);

        Doctor doctor1 = new Doctor("Gregory", "House", Speciality.Cardiology, "DOC1001", "0509998877");
        Doctor doctor2 = new Doctor("James", "Wilson", Speciality.General, "DOC1002", "0678887766");

        clinic.Doctors.Add(doctor1);
        clinic.Doctors.Add(doctor2);

        Appointment app1 = new Appointment(patient1.Id, doctor1.Id, DateTime.Now.AddHours(2), 40);
        clinic.Appointments.Add(app1);

        Console.WriteLine("=== Task 04 Demo ===");

        Doctor[] cardiologists = clinic.Doctors.FindBySpeciality(Speciality.Cardiology);
        Console.WriteLine("Cardiologists count: " + cardiologists.Length);

        if (clinic.Patients.TryFindById(1, out Patient foundPatient))
        {
            Console.WriteLine("Found: " + foundPatient.FullName);
        }

        string patientName = clinic.Patients.FindById(99)?.FullName ?? "not found";
        Console.WriteLine("Patient #99: " + patientName);
    }
}
