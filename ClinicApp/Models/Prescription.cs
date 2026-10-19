using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Prescription : MedicalRecord
{
    private string _medicationName = "";
    private string _dosage = "";
    private int _durationDays;

    public string MedicationName
    {
        get { return _medicationName; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(MedicationName));
            _medicationName = value;
        }
    }

    public string Dosage
    {
        get { return _dosage; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(Dosage));
            _dosage = value;
        }
    }

    public int DurationDays
    {
        get { return _durationDays; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(DurationDays));
            }

            _durationDays = value;
        }
    }

    public string Instructions { get; set; } = "";

    public DateTime ExpiresAt
    {
        get { return Date.AddDays(DurationDays); }
    }

    public Prescription(
        int patientId,
        int doctorId,
        DateTime date,
        string medicationName,
        string dosage,
        int durationDays,
        string instructions = "")
        : base(patientId, doctorId, date)
    {
        MedicationName = medicationName;
        Dosage = dosage;
        DurationDays = durationDays;
        Instructions = instructions;
    }

    public override string GetSummary()
    {
        string summary = MedicationName + ", дозування: " + Dosage;
        summary += ", тривалість: " + DurationDays + " дн.";
        summary += ", до: " + ExpiresAt.ToString("dd.MM.yyyy");

        if (!string.IsNullOrWhiteSpace(Instructions))
        {
            summary += ", інструкції: " + Instructions;
        }

        return summary;
    }

    public override string GetRecordType()
    {
        return "Рецепт";
    }

    public override bool IsActive()
    {
        return DateTime.Today <= ExpiresAt.Date;
    }
}
