using System.Text;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private readonly Clinic _clinic;
    private readonly string _baseDir;

    public ClinicExporter(Clinic clinic, string baseDir = "reports")
    {
        _clinic = clinic;
        _baseDir = baseDir;
    }

    private string CreateReportPath(string fileName)
    {
        string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
        string folderPath = Path.Combine(_baseDir, dateFolder);

        Directory.CreateDirectory(folderPath);

        return Path.Combine(folderPath, fileName);
    }

    public string ExportPatients()
    {
        string path = CreateReportPath("patients.txt");
        Patient[] patients = _clinic.Patients.GetAll();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            writer.WriteLine("PATIENTS REPORT");
            writer.WriteLine("Generated: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            writer.WriteLine("----------------------------------------");

            for (int i = 0; i < patients.Length; i++)
                writer.WriteLine((i + 1) + ". " + patients[i]);

            writer.WriteLine("----------------------------------------");
            writer.WriteLine("Total patients: " + patients.Length);
        }

        return path;
    }

    public string ExportAppointments()
    {
        string path = CreateReportPath("appointments.txt");
        Appointment[] appointments = _clinic.Appointments.GetAll();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            writer.WriteLine("APPOINTMENTS REPORT");
            writer.WriteLine("Generated: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            writer.WriteLine("----------------------------------------");

            for (int i = 0; i < appointments.Length; i++)
                writer.WriteLine((i + 1) + ". " + appointments[i]);

            writer.WriteLine("----------------------------------------");
            writer.WriteLine("Total appointments: " + appointments.Length);
        }

        return path;
    }

    public string ExportBilling()
    {
        string path = CreateReportPath("billing.txt");
        var unpaid = _clinic.Billing.GetAllUnpaid();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            writer.WriteLine("BILLING REPORT");
            writer.WriteLine("Generated: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            writer.WriteLine("----------------------------------------");

            for (int i = 0; i < unpaid.Length; i++)
            {
                writer.WriteLine((i + 1) + ". " + unpaid[i]);
                writer.WriteLine("   Amount: " + unpaid[i].GetCost().ToString("F2") + " UAH");
            }

            writer.WriteLine("----------------------------------------");
            writer.WriteLine("Unpaid appointments: " + unpaid.Length);
            writer.WriteLine("Total debt: " + _clinic.Billing.GetTotalDebt().ToString("F2") + " UAH");
        }

        return path;
    }

    public string ExportTreatmentPlans()
    {
        string path = CreateReportPath("treatment_plans.txt");
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetAll();

        using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
        {
            writer.WriteLine("TREATMENT PLANS REPORT");
            writer.WriteLine("Generated: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            writer.WriteLine("----------------------------------------");

            for (int i = 0; i < plans.Length; i++)
                writer.WriteLine((i + 1) + ". " + plans[i]);

            writer.WriteLine("----------------------------------------");
            writer.WriteLine("Total treatment plans: " + plans.Length);
        }

        return path;
    }

    public string[] ExportAll()
    {
        string[] paths = new string[4];

        paths[0] = ExportPatients();
        paths[1] = ExportAppointments();
        paths[2] = ExportBilling();
        paths[3] = ExportTreatmentPlans();

        return paths;
    }
}
