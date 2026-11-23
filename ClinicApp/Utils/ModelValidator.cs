using System;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ValidationResult result = new ValidationResult();

        foreach (PropertyInfo prop in obj.GetType().GetProperties())
        {
            object? value = prop.GetValue(obj);

            RequiredAttribute? required = prop.GetCustomAttribute<RequiredAttribute>();
            if (required != null && (value == null || (value is string text && string.IsNullOrWhiteSpace(text))))
            {
                result.AddError($"{prop.Name}: {required.ErrorMessage}");
            }

            MaxLengthAttribute? maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLength != null && value is string str && str.Length > maxLength.Length)
            {
                result.AddError($"{prop.Name}: {maxLength.ErrorMessage}");
            }

            MinValueAttribute? minValue = prop.GetCustomAttribute<MinValueAttribute>();
            if (minValue != null && value != null && Convert.ToDouble(value) < minValue.Min)
            {
                result.AddError($"{prop.Name}: {minValue.ErrorMessage}");
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        Console.WriteLine(type.Name);

        foreach (PropertyInfo prop in type.GetProperties())
        {
            Console.Write($"  {prop.Name}");

            foreach (object attribute in prop.GetCustomAttributes())
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
