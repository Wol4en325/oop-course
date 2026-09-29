namespace ClinicApp;

public class AppointmentManager
{
    private const int MaxAppointments = 500;
    private Appointment[] _appointments = new Appointment[MaxAppointments];
    private int _count = 0;
    private PatientManager _patients;
    private DoctorManager _doctors;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public AppointmentManager(PatientManager patients, DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;
    }

    public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes)
    {
        Patient? patient = _patients.FindById(patientId);
        Doctor? doctor = _doctors.FindById(doctorId);

        if (patient == null)
        {
            Console.WriteLine("Error: patient with ID " + patientId + " not found.");
            return false;
        }

        if (doctor == null)
        {
            Console.WriteLine("Error: doctor with ID " + doctorId + " not found.");
            return false;
        }

        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Appointment limit reached.");
            return false;
        }

        Appointment appointment = new Appointment(
            patientId,
            doctorId,
            scheduledAt,
            durationMinutes
        );

        _appointments[_count] = appointment;
        _count++;

        Console.WriteLine(
            "Appointment [" + appointment.Id + "] created: " +
            patient.FullName + " -> " +
            doctor.FullName + " at " +
            scheduledAt.ToString("dd.MM.yyyy HH:mm")
        );

        return true;
    }

    public bool Cancel(int id, string reason)
    {
        Appointment? appointment = FindById(id);

        if (appointment == null)
        {
            return false;
        }

        return appointment.Cancel(reason);
    }

    public bool Complete(int id)
    {
        Appointment? appointment = FindById(id);

        if (appointment == null)
        {
            return false;
        }

        return appointment.Complete();
    }

    public Appointment[] GetByPatient(int patientId)
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                foundCount++;
            }
        }

        Appointment[] result = new Appointment[foundCount];
        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[resultIndex] = _appointments[i];
                resultIndex++;
            }
        }

        return result;
    }

    public Appointment[] GetByDoctor(int doctorId)
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                foundCount++;
            }
        }

        Appointment[] result = new Appointment[foundCount];
        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                result[resultIndex] = _appointments[i];
                resultIndex++;
            }
        }

        return result;
    }

    public Appointment[] GetByDate(DateTime date)
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                foundCount++;
            }
        }

        Appointment[] result = new Appointment[foundCount];
        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                result[resultIndex] = _appointments[i];
                resultIndex++;
            }
        }

        return result;
    }

    public Appointment[] GetUpcoming()
    {
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                foundCount++;
            }
        }

        Appointment[] result = new Appointment[foundCount];
        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                result[resultIndex] = _appointments[i];
                resultIndex++;
            }
        }

        return result;
    }

    public void DisplayAppointment(Appointment appointment)
    {
        Patient? patient = _patients.FindById(appointment.PatientId);
        Doctor? doctor = _doctors.FindById(appointment.DoctorId);

        string patientName;
        string doctorName;

        if (patient == null)
        {
            patientName = "Patient #" + appointment.PatientId;
        }
        else
        {
            patientName = patient.FullName;
        }

        if (doctor == null)
        {
            doctorName = "Doctor #" + appointment.DoctorId;
        }
        else
        {
            doctorName = doctor.FullName;
        }

        string result = "[" + appointment.Id + "] ";
        result += patientName + " -> " + doctorName;
        result += " | " + appointment.ScheduledAt.ToString("dd.MM.yyyy HH:mm");
        result += "-" + appointment.EndsAt.ToString("HH:mm");
        result += " | " + appointment.Status;

        if (appointment.Notes.Length > 0)
        {
            result += " | " + appointment.Notes;
        }

        Console.WriteLine(result);
    }

    public void DisplayList(Appointment[] appointments)
    {
        if (appointments.Length == 0)
        {
            Console.WriteLine("No appointments found.");
            return;
        }

        for (int i = 0; i < appointments.Length; i++)
        {
            DisplayAppointment(appointments[i]);
        }
    }

    private Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id)
            {
                return _appointments[i];
            }
        }

        return null;
    }
}
