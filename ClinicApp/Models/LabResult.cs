using ClinicApp.Utils;

namespace ClinicApp.Models;

public class LabResult : MedicalRecord
{
    private string _testName = "";
    private double _value;
    private string _unit = "";
    private string _referenceRange = "";

    public string TestName
    {
        get { return _testName; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(TestName));
            _testName = value;
        }
    }

    public double Value
    {
        get { return _value; }
        set
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentOutOfRangeException(nameof(Value));
            }

            _value = value;
        }
    }

    public string Unit
    {
        get { return _unit; }
        set { _unit = value ?? ""; }
    }

    public string ReferenceRange
    {
        get { return _referenceRange; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(ReferenceRange));
            _referenceRange = value;
        }
    }

    public bool IsNormal { get; set; }

    public LabResult(
        int patientId,
        int doctorId,
        DateTime date,
        string testName,
        double value,
        string unit,
        string referenceRange,
        bool isNormal)
        : base(patientId, doctorId, date)
    {
        TestName = testName;
        Value = value;
        Unit = unit;
        ReferenceRange = referenceRange;
        IsNormal = isNormal;
    }

    public override string GetSummary()
    {
        string summary = TestName + ": " + Value + " " + Unit;
        summary += " (норма: " + ReferenceRange + ")";

        if (IsNormal)
        {
            summary += " [у нормі]";
        }
        else
        {
            summary += " [поза нормою]";
        }

        return summary;
    }

    public override string GetRecordType()
    {
        return "Аналіз";
    }
}
