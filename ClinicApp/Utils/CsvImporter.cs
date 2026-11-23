using System.Globalization;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class CsvImporter
{
    public ImportResult ImportPatients(Clinic clinic, string filePath)
    {
        ImportResult result = new ImportResult();

        if (!File.Exists(filePath))
        {
            result.AddError(0, "File not found: " + filePath);
            return result;
        }

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            try
            {
                string[] fields = lines[i].Split(',');

                if (fields.Length != 5)
                    throw new FormatException("Expected 5 columns.");

                string firstName = fields[0].Trim();
                string lastName = fields[1].Trim();

                DateTime dateOfBirth = DateTime.ParseExact(
                    fields[2].Trim(),
                    "dd.MM.yyyy",
                    CultureInfo.InvariantCulture
                );

                BloodType bloodType = (BloodType)Enum.Parse(
                    typeof(BloodType),
                    fields[3].Trim()
                );

                if (!Enum.IsDefined(typeof(BloodType), bloodType))
                    throw new FormatException("Invalid blood type.");

                string phone = fields[4].Trim();

                Patient patient = new Patient(
                    firstName,
                    lastName,
                    dateOfBirth,
                    bloodType,
                    phone
                );

                clinic.Patients.Add(patient);
                result.AddSuccess();
            }
            catch (Exception ex)
            {
                result.AddError(i + 1, ex.Message);
            }
        }

        return result;
    }
}
