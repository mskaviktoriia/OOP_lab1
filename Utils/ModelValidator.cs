using System;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ValidationResult result = new ValidationResult();

        PropertyInfo[] properties = obj.GetType().GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo prop = properties[i];
            object value = prop.GetValue(obj);

            RequiredAttribute required = prop.GetCustomAttribute<RequiredAttribute>();
            if (required != null)
            {
                bool empty = false;

                if (value == null)
                    empty = true;
                else if (value is string text && text.Trim().Length == 0)
                    empty = true;

                if (empty)
                    result.AddError(prop.Name + ": " + required.ErrorMessage);
            }

            MaxLengthAttribute maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLength != null && value is string str)
            {
                if (str.Length > maxLength.Length)
                    result.AddError(prop.Name + ": " + maxLength.ErrorMessage);
            }

            MinValueAttribute minValue = prop.GetCustomAttribute<MinValueAttribute>();
            if (minValue != null && value != null)
            {
                double number = Convert.ToDouble(value);

                if (number < minValue.Min)
                    result.AddError(prop.Name + ": " + minValue.ErrorMessage);
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        Console.WriteLine("Тип: " + type.Name);

        PropertyInfo[] properties = type.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo prop = properties[i];
            string line = "  " + prop.Name + " (" + prop.PropertyType.Name + ")";

            string attrs = "";

            foreach (Attribute attr in prop.GetCustomAttributes())
            {
                if (attrs.Length > 0)
                    attrs += ", ";

                attrs += attr.GetType().Name;
            }

            if (attrs.Length > 0)
                line += " [" + attrs + "]";

            Console.WriteLine(line);
        }
    }
}