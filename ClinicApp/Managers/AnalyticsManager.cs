using System.Collections.Generic;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class AnalyticsManager
{
    private readonly AppointmentManager _appointments;
    private readonly DoctorManager _doctors;
    private readonly PatientManager _patients;

    public AnalyticsManager(AppointmentManager appointments, DoctorManager doctors, PatientManager patients)
    {
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
    }

    public IEnumerable<DoctorStats> ComputeDoctorStats()
    {
        Appointment[] appointments = _appointments.GetAll();

        for (int i = 0; i < _doctors.Count; i++)
        {
            Doctor? doctor = _doctors[i];

            if (doctor == null)
            {
                continue;
            }

            int appointmentCount = 0;
            decimal totalRevenue = 0;
            DateTime lastAppointmentDate = DateTime.MinValue;

            for (int j = 0; j < appointments.Length; j++)
            {
                if (appointments[j].DoctorId == doctor.Id)
                {
                    appointmentCount++;
                    totalRevenue += appointments[j].GetCost();

                    if (appointments[j].ScheduledAt > lastAppointmentDate)
                    {
                        lastAppointmentDate = appointments[j].ScheduledAt;
                    }
                }
            }

            yield return new DoctorStats(
                doctor.Id,
                doctor.FullName,
                appointmentCount,
                totalRevenue,
                lastAppointmentDate
            );
        }
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        Appointment[] appointments = _appointments.GetAll();
        Patient[] patients = _patients.GetAll();

        for (int i = 0; i < patients.Length; i++)
        {
            int visitCount = 0;
            decimal totalSpent = 0;
            DateTime lastVisitDate = DateTime.MinValue;

            for (int j = 0; j < appointments.Length; j++)
            {
                if (appointments[j].PatientId == patients[i].Id)
                {
                    visitCount++;
                    totalSpent += appointments[j].GetCost();

                    if (appointments[j].ScheduledAt > lastVisitDate)
                    {
                        lastVisitDate = appointments[j].ScheduledAt;
                    }
                }
            }

            yield return new PatientStats(
                patients[i].Id,
                patients[i].FullName,
                visitCount,
                totalSpent,
                lastVisitDate
            );
        }
    }
}
