using System.Text.RegularExpressions;

namespace ClinicApp.Utils;

public static class ClinicValidator
{
    private static readonly Regex PhoneRegex = new(@"\A[0-9]{10}\z");
    private static readonly Regex EmailRegex = new(@"\A[^@\s]+@[^@\s]+\.[^@\s]+\z");

    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            throw new ArgumentException(fieldName + " must contain 1 to 50 characters.", fieldName);
    }

    public static void ValidatePhone(string phone)
    {
        if (phone == null || !PhoneRegex.IsMatch(phone))
            throw new ArgumentException("Phone must contain exactly 10 digits.", nameof(phone));
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return;

        if (!EmailRegex.IsMatch(email))
            throw new ArgumentException("Invalid email address.", nameof(email));
    }

    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value.Date > DateTime.Today)
            throw new ArgumentOutOfRangeException(fieldName, "Date cannot be in the future.");

        if (value.Year < 1900)
            throw new ArgumentOutOfRangeException(fieldName, "Date cannot be earlier than 1900.");
    }

    public static void ValidatePositive(int value, string fieldName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(fieldName, "Value must be greater than zero.");
    }
}