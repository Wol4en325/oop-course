using ClinicApp.Utils;

namespace ClinicApp.Models;

public class LabResult : MedicalRecord
{
    private string _testName = "";
    private string _result = "";

    public string TestName
    {
        get { return _testName; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(TestName));
            _testName = value;
        }
    }

    public string Result
    {
        get { return _result; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(Result));
            _result = value;
        }
    }

    public string Unit { get; set; }

    public LabResult(
        int patientId,
        int doctorId,
        DateTime date,
        string testName,
        string result,
        string unit = "")
        : base(patientId, doctorId, date)
    {
        TestName = testName;
        Result = result;
        Unit = unit;
    }

    public override string GetSummary()
    {
        string summary = TestName + ": " + Result;

        if (Unit.Length > 0)
        {
            summary += " " + Unit;
        }

        return summary;
    }

    public override string GetRecordType()
    {
        return "Lab Result";
    }
}
