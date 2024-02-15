using Lexicom.Wpf.ValueConverters.Abstractions;

namespace Lexicom.Wpf.ValueConverters;
public sealed class ToBooleanConverter : ValueConverterBase<bool>
{
    private const string TRUE_SHORT = "t";
    private const string FALSE_SHORT = "f";
    private static readonly string TRUE_FULL = GetBooleanString(true);
    private static readonly string FALSE_FULL = GetBooleanString(false);

    private static ValueConverterParameterDefinition InvertParameter { get; } = new ValueConverterParameterDefinition("invert");
    private static ValueConverterParameterDefinition<IsStrings> FalseWhenStringParameter { get; } = new ValueConverterParameterDefinition<IsStrings>("falsewhenstring",
    [
        new ResultForPatternMatch<IsStrings>(IsStrings.NullOrEmpty, ["isnullorempty", "nullorempty", "noe", "0"]),
        new ResultForPatternMatch<IsStrings>(IsStrings.NullOrWhiteSpace, ["isnullorwhitespace", "nullorwhitespace", "now", "1"])
    ]);

    private static string GetBooleanString(bool value) => value.ToString().ToLowerInvariant();
    private static T GetBooleanDichotomy<T>(bool value, T trueValue, T falseValue) => value ? trueValue : falseValue;
    private static bool GetBooleanFromComparable(IComparable comparableValue, bool trueResult, bool falseResult) => comparableValue.CompareTo(0) == 1 ? trueResult : falseResult;

    public ToBooleanConverter()
    {
        TrueResult = true;
        FalseResult = false;
    }

    public bool TrueResult { get; set; }
    public bool FalseResult { get; set; }

    public bool? DefaultIsInverted { get; set; }
    public IsStrings? DefaultFalseWhenString { get; set; }

    protected override bool TConvert(object? value, ValueConverterArgs args)
    {
        bool trueResult = TrueResult;
        bool falseResult = FalseResult;

        bool result = GetResultFromObject(value, trueResult, falseResult);

        if (HasParameter(InvertParameter))
        {
            return result == trueResult ? FalseResult : trueResult;
        }
        else if (DefaultIsInverted is not null && DefaultIsInverted.Value)
        {
            return result == trueResult ? FalseResult : trueResult;
        }

        return result == trueResult ? trueResult : falseResult;
    }

    private bool GetResultFromObject(object? value, bool trueResult, bool falseResult)
    {
        if (value is null)
        {
            return falseResult;
        }

        if (value is string stringValue)
        {
            IsStrings isString = IsStrings.None;
            if (HasParameter(FalseWhenStringParameter, out IsStrings isStringParameter))
            {
                isString = isStringParameter;
            }
            else if (DefaultFalseWhenString is not null)
            {
                isString = DefaultFalseWhenString.Value;
            }

            if (isString == IsStrings.NullOrEmpty && string.IsNullOrEmpty(stringValue))
            {
                return falseResult;
            }
            else if (isString == IsStrings.NullOrWhiteSpace && string.IsNullOrWhiteSpace(stringValue))
            {
                return falseResult;
            }

            if (string.Equals(stringValue, TRUE_FULL, StringComparison.OrdinalIgnoreCase))
            {
                return trueResult;
            }
            if (string.Equals(stringValue, TRUE_SHORT, StringComparison.OrdinalIgnoreCase))
            {
                return trueResult;
            }

            if (string.Equals(stringValue, FALSE_FULL, StringComparison.OrdinalIgnoreCase))
            {
                return falseResult;
            }
            if (string.Equals(stringValue, FALSE_SHORT, StringComparison.OrdinalIgnoreCase))
            {
                return falseResult;
            }

            if (bool.TryParse(stringValue, out bool boolStringValue))
            {
                value = boolStringValue;
            }
            else if (int.TryParse(stringValue, out int intStringValue))
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
                return trueResult;
            }
        }

        if (value is bool boolValue)
        {
            return boolValue ? trueResult : falseResult;
        }

        if (value is IComparable comparableValue)
        {
            return GetBooleanFromComparable(comparableValue, trueResult, falseResult);
        }

        return TrueResult;
    }

    protected override object? TConvertBack(bool value, ValueConverterArgs args)
    {
        if (args.TargetType == typeof(bool))
        {
            return value;
        }

        if (args.TargetType == typeof(string))
        {
            return GetBooleanString(value);
        }

        if (args.TargetType.IsEnum)
        {
            Array enumValues = Enum.GetValues(args.TargetType);

            if (enumValues.Length >= 2)
            {
                return GetBooleanDichotomy(value, enumValues.GetValue(1), enumValues.GetValue(0));
            }
            else if (enumValues.Length >= 1)
            {
                return GetBooleanDichotomy(value, enumValues.GetValue(0), null);
            }

            return null;
        }

        if (args.TargetType == typeof(short))
        {
            return GetBooleanDichotomy<short>(value, 1, 0);
        }
        if (args.TargetType == typeof(int))
        {
            return GetBooleanDichotomy(value, 1, 0);
        }
        if (args.TargetType == typeof(long))
        {
            return GetBooleanDichotomy<long>(value, 1, 0);
        }
        if (args.TargetType == typeof(float))
        {
            return GetBooleanDichotomy<float>(value, 1, 0);
        }
        if (args.TargetType == typeof(double))
        {
            return GetBooleanDichotomy<double>(value, 1, 0);
        }
        if (args.TargetType == typeof(decimal))
        {
            return GetBooleanDichotomy<decimal>(value, 1, 0);
        }

        return null;
    }
}