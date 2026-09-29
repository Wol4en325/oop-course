namespace ClinicApp;

public class Clinic
{
    public string Name { get; }
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }

    public Clinic(string name)
    {
        Name = name;
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
    }

    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine();
        Console.WriteLine("=== Schedule for " + date.ToString("dd.MM.yyyy") + " ===");

        Appointment[] appointments = Appointments.GetByDate(date);
        Appointments.DisplayList(appointments);
    }

    public void GenerateReport()
    {
        Appointment[] upcomingAppointments = Appointments.GetUpcoming();
        Doctor[] doctors = Doctors.GetAll();

        Console.WriteLine();
        Console.WriteLine("ã==============================================¬");
        Console.WriteLine("¦  Report - " + Name);
        Console.WriteLine("¦==============================================¦");
        Console.WriteLine("¦  Patients:          " + Patients.Count);
        Console.WriteLine("¦  Doctors:           " + Doctors.Count);
        Console.WriteLine("¦  Upcoming appointments: " + upcomingAppointments.Length);
        Console.WriteLine("¦==============================================¦");
        Console.WriteLine("¦  Doctor workload (upcoming appointments):");

        for (int i = 0; i < doctors.Length; i++)
        {
            int appointmentCount = 0;

            for (int j = 0; j < upcomingAppointments.Length; j++)
            {
                if (upcomingAppointments[j].DoctorId == doctors[i].Id)
                {
                    appointmentCount++;
                }
            }

            Console.WriteLine(
                "¦    " + doctors[i].FullName +
                " (" + doctors[i].Speciality + "): " +
                appointmentCount + " appointments"
            );
        }

        Console.WriteLine("L==============================================-");
    }
}
