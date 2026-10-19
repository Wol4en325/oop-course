using ClinicApp.Enums;
using ClinicApp.Utils;
using ClinicApp.Models;
namespace ClinicApp.Managers;

public class AppointmentManager
{
    private const int MaxAppointments = 100;
    private Appointment[] _appointments = new Appointment[MaxAppointments];
    private int _count = 0;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public Appointment? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }
            return _appointments[index];
        }
    }

    public void Add(Appointment appointment)
    {
        if (_count >= MaxAppointments)
        {
            return;
        }

        _appointments[_count] = appointment;
        _count++;
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
        int idx = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                result[idx++] = _appointments[i];
            }
        }

        return result;
    }

    public Appointment[] GetByDate(int year, int month, int day)
    {
        return GetByDate(new DateTime(year, month, day));
    }
}


