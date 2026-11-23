using System;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ValidationResult result = new ValidationResult();
        Type type = obj.GetType();

        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            object? value = prop.GetValue(obj);

            RequiredAttribute? required = prop.GetCustomAttribute<RequiredAttribute>();

            if (required != null)
            {
                if (value == null || (value is string text && string.IsNullOrWhiteSpace(text)))
                {
                    result.AddError($"{prop.Name}: {required.ErrorMessage}");
                }
            }

            MaxLengthAttribute? maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();

            if (maxLength != null && value is string str)
            {
                if (str.Length > maxLength.Length)
                {
                    result.AddError($"{prop.Name}: {maxLength.ErrorMessage}");
                }
            }

            MinValueAttribute? minValue = prop.GetCustomAttribute<MinValueAttribute>();

            if (minValue != null && value != null)
            {
                double number = Convert.ToDouble(value);

                if (number < minValue.Min)
                {
                    result.AddError($"{prop.Name}: {minValue.ErrorMessage}");
                }
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        Console.WriteLine(type.Name);

        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            Console.Write($"  {prop.Name}");

            object[] attributes = prop.GetCustomAttributes().ToArray();

            foreach (object attribute in attributes)
            {
                string attributeName = attribute.GetType().Name;

                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName.Substring(0, attributeName.Length - 9);
                }

                Console.Write($" [{attributeName}]");
            }

            Console.WriteLine();
        }
    }
}
