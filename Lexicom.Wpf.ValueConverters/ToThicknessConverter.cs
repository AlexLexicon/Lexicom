using Lexicom.Wpf.ValueConverters.Abstractions;
using System.Windows;

namespace Lexicom.Wpf.ValueConverters;
public class ToThicknessConverter : ValueConverterBase<Thickness>
{
    private static Thickness GetResultFromObject(object? value)
    {
        if (value is Thickness thicknessValue)
        {
            return thicknessValue;
        }

        if (value is null)
        {
            return new Thickness();
        }

        if (value is string stringValue)
        {
            if (int.TryParse(stringValue, out int intStringValue))
            {
                value = intStringValue;
            }
            else if (long.TryParse(stringValue, out long longStringValue))
            {
                value = longStringValue;
            }
            else if (double.TryParse(stringValue, out double doubleStringValue))
            {
                value = doubleStringValue;
            }
            else if (decimal.TryParse(stringValue, out decimal decimalStringValue))
            {
                value = decimalStringValue;
            }
            else
            {
                return new Thickness();
            }
        }

        if (value is short shortValue)
        {
            return new Thickness(shortValue);
        }
        if (value is int intValue)
        {
            return new Thickness(intValue);
        }
        if (value is long longValue)
        {
            return new Thickness(longValue);
        }
        if (value is float floatValue)
        {
            return new Thickness(floatValue);
        }
        if (value is double doubleValue)
        {
            return new Thickness(doubleValue);
        }
        if (value is decimal decimalValue)
        {
            return new Thickness((double)decimalValue);
        }

        return new Thickness();
    }

    protected override Thickness TConvert(object? value, ValueConverterArgs args)
    {
        return GetResultFromObject(value);
    }

    protected override object? TConvertBack(Thickness value, ValueConverterArgs args)
    {
        if (args.TargetType == typeof(Thickness))
        {
            return value;
        }

        if (args.TargetType == typeof(string))
        {
            return value.ToString();
        }

        if (args.TargetType == typeof(short))
        {
            return (short)value.Left;
        }
        if (args.TargetType == typeof(int))
        {
            return (int)value.Left;
        }
        if (args.TargetType == typeof(long))
        {
            return (long)value.Left;
        }
        if (args.TargetType == typeof(float))
        {
            return (float)value.Left;
        }
        if (args.TargetType == typeof(double))
        {
            return value.Left;
        }
        if (args.TargetType == typeof(decimal))
        {
            return (decimal)value.Left;
        }

        return null;
    }
}
