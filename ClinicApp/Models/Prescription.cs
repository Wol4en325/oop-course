using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Prescription : MedicalRecord
{
    private string _medicationName = "";
    private string _instructions = "";

    public string MedicationName
    {
        get { return _medicationName; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(MedicationName));
            _medicationName = value;
        }
    }

    public string Instructions
    {
        get { return _instructions; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(Instructions));
            _instructions = value;
        }
    }

    public DateTime? EndDate { get; set; }

    public Prescription(
        int patientId,
        int doctorId,
        DateTime date,
        string medicationName,
        string instructions,
        DateTime? endDate = null)
        : base(patientId, doctorId, date)
    {
        MedicationName = medicationName;
        Instructions = instructions;
        EndDate = endDate;
    }

    public override string GetSummary()
    {
        string summary = MedicationName + ": " + Instructions;

        if (EndDate.HasValue)
        {
            summary += " (до " + EndDate.Value.ToString("dd.MM.yyyy") + ")";
        }

        return summary;
    }

    public override string GetRecordType()
    {
        return "Prescription";
    }

    public override bool IsActive()
    {
        return !EndDate.HasValue || EndDate.Value.Date >= DateTime.Today;
    }
}
