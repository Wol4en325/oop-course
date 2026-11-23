using System;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class FormBuilder
{
    public static T Build<T>() where T : new()
    {
        T obj = new T();
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            if (prop.GetSetMethod() == null)
            {
                continue;
            }

            string hint = "";

            RequiredAttribute? required = prop.GetCustomAttribute<RequiredAttribute>();
            if (required != null)
            {
                hint += "Required";
            }

            MaxLengthAttribute? maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLength != null)
            {
                if (hint.Length > 0)
                {
                    hint += ", ";
                }

                hint += $"Max length: {maxLength.Length}";
            }

            MinValueAttribute? minValue = prop.GetCustomAttribute<MinValueAttribute>();
            if (minValue != null)
            {
                if (hint.Length > 0)
                {
                    hint += ", ";
                }

                hint += $"Minimum value: {minValue.Min}";
            }

            while (true)
            {
                if (hint.Length > 0)
                {
                    Console.Write($"{prop.Name} ({hint}): ");
                }
                else
                {
                    Console.Write($"{prop.Name}: ");
                }

                string input = Console.ReadLine() ?? "";

                try
                {
                    object? value = Convert.ChangeType(input, prop.PropertyType);
                    prop.SetValue(obj, value);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }

        return obj;
    }
}
