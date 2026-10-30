using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointmentManager;

    public BillingManager(AppointmentManager appointmentManager)
    {
        _appointmentManager = appointmentManager;
    }

    public IPayable[] GetAllUnpaid()
    {
        return GetUnpaid(_appointmentManager.GetAll(), 0, false);
    }

    public IPayable[] GetUnpaidByPatient(int patientId)
    {
        return GetUnpaid(_appointmentManager.GetByPatient(patientId), 0, false);
    }

    private IPayable[] GetUnpaid(Appointment[] appointments, int patientId, bool filterByPatient)
    {
        int count = 0;

        for (int i = 0; i < appointments.Length; i++)
        {
            if (!appointments[i].IsPaid &&
                !appointments[i].IsCancelled &&
                (!filterByPatient || appointments[i].PatientId == patientId))
            {
                count++;
            }
        }

        IPayable[] result = new IPayable[count];
        int index = 0;

        for (int i = 0; i < appointments.Length; i++)
        {
            if (!appointments[i].IsPaid &&
                !appointments[i].IsCancelled &&
                (!filterByPatient || appointments[i].PatientId == patientId))
            {
                result[index++] = appointments[i];
            }
        }

        return result;
    }

    public decimal GetTotalDebt()
    {
        IPayable[] unpaid = GetAllUnpaid();
        decimal total = 0m;

        for (int i = 0; i < unpaid.Length; i++)
        {
            total += unpaid[i].GetCost();
        }

        return total;
    }

    public decimal GetPatientDebt(int patientId)
    {
        IPayable[] unpaid = GetUnpaidByPatient(patientId);
        decimal total = 0m;

        for (int i = 0; i < unpaid.Length; i++)
        {
            total += unpaid[i].GetCost();
        }

        return total;
    }

    public bool PayAppointment(int appointmentId)
    {
        Appointment[] appointments = _appointmentManager.GetAll();

        for (int i = 0; i < appointments.Length; i++)
        {
            if (appointments[i].Id == appointmentId)
            {
                if (appointments[i].IsPaid || appointments[i].IsCancelled)
                {
                    return false;
                }

                appointments[i].MarkPaid();
                return true;
            }
        }

        return false;
    }

    public void DisplayUnpaid(IPayable[] items)
    {
        if (items.Length == 0)
        {
            Console.WriteLine("\u041D\u0435\u043C\u0430\u0454\u0020\u043D\u0435\u043E\u043F\u043B\u0430\u0447\u0435\u043D\u0438\u0445\u0020\u0437\u0430\u043F\u0438\u0441\u0456\u0432.");
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] is Appointment appointment)
            {
                Console.WriteLine(appointment + " | \u0421\u0443\u043C\u0430\u003A " + items[i].GetCost().ToString("F2") + " \u0433\u0440\u043D");
            }
            else
            {
                Console.WriteLine("Appointment #" + (i + 1) + " | \u0421\u0443\u043C\u0430\u003A " + items[i].GetCost().ToString("F2") + " \u0433\u0440\u043D");
            }
        }
    }
}