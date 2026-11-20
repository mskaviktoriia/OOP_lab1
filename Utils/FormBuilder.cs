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

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo prop = properties[i];

            if (prop.GetSetMethod() == null)
                continue;

            string hint = "";

            if (prop.GetCustomAttribute<RequiredAttribute>() != null)
                hint += "обов'язкове";

            MaxLengthAttribute maxLength = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLength != null)
            {
                if (hint.Length > 0)
                    hint += ", ";

                hint += "макс. " + maxLength.Length;
            }

            MinValueAttribute minValue = prop.GetCustomAttribute<MinValueAttribute>();
            if (minValue != null)
            {
                if (hint.Length > 0)
                    hint += ", ";

                hint += "мін. " + minValue.Min;
            }

            string label = "  " + prop.Name;

            if (hint.Length > 0)
                label += " (" + hint + ")";

            bool ok = false;

            while (!ok)
            {
                Console.Write(label + ": ");
                string input = Console.ReadLine();

                try
                {
                    object converted = Convert.ChangeType(input, prop.PropertyType);
                    prop.SetValue(obj, converted);
                    ok = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Некоректне значення, спробуйте ще раз.");
                }
            }
        }

        return obj;
    }
}