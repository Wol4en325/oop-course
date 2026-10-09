namespace ClinicApp.Utils;

public static class ClinicValidator
{
    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            throw new ArgumentException(fieldName + " must contain 1 to 50 characters.", fieldName);
    }

    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10)
            throw new ArgumentException("Phone must contain exactly 10 digits.", nameof(phone));

        for (int i = 0; i < phone.Length; i++)
        {
            if (phone[i] < '0' || phone[i] > '9')
                throw new ArgumentException("Phone must contain only digits.", nameof(phone));
        }
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
