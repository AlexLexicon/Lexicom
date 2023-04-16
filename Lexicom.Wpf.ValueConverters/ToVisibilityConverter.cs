using Lexicom.Wpf.ValueConverters.Abstractions;
using System.Collections;
using System.Windows;

namespace Lexicom.Wpf.ValueConverters;
public sealed class ToVisibilityConverter : ValueConverterBase<Visibility>
{
    private const string VISIBLE_SHORT = "v";
    private const string HIDDEN_SHORT = "h";
    private const string COLLAPSED_SHORT = "c";
    private static readonly string VISIBLE_FULL = GetVisibilityString(Visibility.Visible);
    private static readonly string HIDDEN_FULL = GetVisibilityString(Visibility.Hidden);
    private static readonly string COLLAPSED_FULL = GetVisibilityString(Visibility.Collapsed);

    private static readonly ResultForPatternMatchCollection<Visibility> VISIBILITY_PARAMETER_PATTERN_MATCHES = new ResultForPatternMatchCollection<Visibility>
    {
         new ResultForPatternMatch<Visibility>(Visibility.Collapsed, new [] { COLLAPSED_FULL, COLLAPSED_SHORT, "0" }),
         new ResultForPatternMatch<Visibility>(Visibility.Hidden, new [] { HIDDEN_FULL, HIDDEN_SHORT, "1", "f", "false" }),
         new ResultForPatternMatch<Visibility>(Visibility.Visible, new [] { VISIBLE_FULL, VISIBLE_SHORT, "2", "t", "true" }),
    };
    private static readonly ValueConverterParameterDefinition INVERT_PARAMETER = new ValueConverterParameterDefinition("invert");
    private static readonly ValueConverterParameterDefinition<Visibility> SHOW_PARAMETER = new ValueConverterParameterDefinition<Visibility>("show", VISIBILITY_PARAMETER_PATTERN_MATCHES);
    private static readonly ValueConverterParameterDefinition<Visibility> HIDE_PARAMETER = new ValueConverterParameterDefinition<Visibility>("hide", VISIBILITY_PARAMETER_PATTERN_MATCHES);
    private static readonly ValueConverterParameterDefinition<IsStrings> HIDE_WHEN_STRING_PARAMETER = new ValueConverterParameterDefinition<IsStrings>("hidewhenstring", new ResultForPatternMatchCollection<IsStrings>
    {
        new ResultForPatternMatch<IsStrings>(IsStrings.NullOrEmpty, new [] { "isnullorempty", "nullorempty", "noe", "0" }),
        new ResultForPatternMatch<IsStrings>(IsStrings.NullOrWhiteSpace, new [] { "isnullorwhitespace", "nullorwhitespace", "now", "1" })
    });
    private static readonly ValueConverterParameterDefinition<IsEnumerable> HIDE_WHEN_ENUMERABLE_PARAMETER = new ValueConverterParameterDefinition<IsEnumerable>("hidewhenenumerable", new ResultForPatternMatchCollection<IsEnumerable>
    {
        new ResultForPatternMatch<IsEnumerable>(IsEnumerable.NullOrEmpty, new [] { "isnullorempty", "nullorempty", "noe", "0" }),
    });

    private static string GetVisibilityString(Visibility value) => value.ToString().ToLowerInvariant();
    private static T GetVisibilityDichotomy<T>(Visibility visibility, T visible, T hiddenOrCollapsed) => GetVisibilityTrichotomy(visibility, visible, hiddenOrCollapsed, hiddenOrCollapsed);
    private static T GetVisibilityTrichotomy<T>(Visibility visibility, T visible, T hidden, T collapsed)
    {
        return visibility switch
        {
            Visibility.Visible => visible,
            Visibility.Collapsed => collapsed,
            _ => hidden,
        };
    }
    private static Visibility GetVisibilityFromComparable(IComparable comparableValue, Visibility showResult, Visibility hideResult)
    {
        return comparableValue.CompareTo(0) > 0 ? showResult : hideResult;
    }

    public ToVisibilityConverter()
    {
        ShowResult = Visibility.Visible;
        HideResult = Visibility.Hidden;
    }

    public Visibility ShowResult { get; set; }
    public Visibility HideResult { get; set; }

    public bool? DefaultIsInverted { get; set; }
    public Visibility? DefaultShow { get; set; }
    public Visibility? DefaultHide { get; set; }
    public IsStrings? DefaultHideWhenString { get; set; }
    public IsEnumerable? DefaultHideWhenEnumerable { get; set; }

    protected override Visibility TConvert(object? value, ValueConverterArgs args)
    {
        Visibility showResult = ShowResult;
        Visibility hideResult = HideResult;

        if (HasParameter(SHOW_PARAMETER, out Visibility showParameterResult))
        {
            showResult = showParameterResult;
        }
        else if (DefaultShow is not null)
        {
            showResult = DefaultShow.Value;
        }

        if (HasParameter(HIDE_PARAMETER, out Visibility hideParameterResult))
        {
            hideResult = hideParameterResult;
        }
        else if (DefaultHide is not null)
        {
            hideResult = DefaultHide.Value;
        }

        Visibility result = GetResultFromObject(value, showResult, hideResult);

        if (HasParameter(INVERT_PARAMETER))
        { 
            return result == showResult ? hideResult : showResult;
        }
        else if (DefaultIsInverted is not null && DefaultIsInverted.Value)
        {
            return result == showResult ? hideResult : showResult;
        }

        return result == showResult ? showResult : hideResult;
    }

    private Visibility GetResultFromObject(object? value, Visibility showResult, Visibility hideResult)
    {
        if (value is Visibility visibilityValue)
        {
            return visibilityValue;
        }

        if (value is null)
        {
            return hideResult;
        }

        if (value is string stringValue)
        {
            IsStrings isString = IsStrings.None;
            if (HasParameter(HIDE_WHEN_STRING_PARAMETER, out IsStrings isStringParameter))
            {
                isString = isStringParameter;
            }
            else if (DefaultHideWhenString is not null)
            {
                isString = DefaultHideWhenString.Value;
            }

            if (isString == IsStrings.NullOrEmpty && string.IsNullOrEmpty(stringValue))
            {
                return hideResult;
            }
            else if (isString == IsStrings.NullOrWhiteSpace && string.IsNullOrWhiteSpace(stringValue))
            {
                return hideResult;
            }

            if (string.Equals(stringValue, VISIBLE_FULL, StringComparison.OrdinalIgnoreCase))
            {
                return showResult;
            }
            if (string.Equals(stringValue, VISIBLE_SHORT, StringComparison.OrdinalIgnoreCase))
            {
                return showResult;
            }

            if (string.Equals(stringValue, HIDDEN_FULL, StringComparison.OrdinalIgnoreCase) || string.Equals(stringValue, COLLAPSED_FULL, StringComparison.OrdinalIgnoreCase))
            {
                return hideResult;
            }
            if (string.Equals(stringValue, HIDDEN_SHORT, StringComparison.OrdinalIgnoreCase) || string.Equals(stringValue, COLLAPSED_SHORT, StringComparison.OrdinalIgnoreCase))
            {
                return hideResult;
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
                return showResult;
            }
        }

        if (value is bool boolValue)
        {
            return boolValue ? showResult : hideResult;
        }

        IsEnumerable isEnumerable = IsEnumerable.None;
        if (HasParameter(HIDE_WHEN_ENUMERABLE_PARAMETER, out IsEnumerable isEnumerableParameter))
        {
            isEnumerable = isEnumerableParameter;
        }
        else if (DefaultHideWhenEnumerable is not null)
        {
            isEnumerable = DefaultHideWhenEnumerable.Value;
        }

        if (isEnumerable == IsEnumerable.NullOrEmpty)
        {
            if (value is ICollection collection)
            {
                return collection.Count > 0 ? showResult : hideResult;
            }
            else if (value is IEnumerable enumerable)
            {
                return enumerable.GetEnumerator().MoveNext() ? showResult : hideResult;
            }
        } 

        if (value is IComparable comparableValue)
        {
            return GetVisibilityFromComparable(comparableValue, showResult, hideResult);
        }

        return showResult;
    }

    protected override object? TConvertBack(Visibility value, ValueConverterArgs args)
    {
        if (args.TargetType == typeof(Visibility))
        {
            return value;
        }

        if (args.TargetType == typeof(string))
        {
            return GetVisibilityString(value);
        }

        if (args.TargetType.IsEnum)
        {
            Array enumValues = Enum.GetValues(args.TargetType);

            if (enumValues.Length >= 3)
            {
                return GetVisibilityTrichotomy(value, enumValues.GetValue(2), enumValues.GetValue(1), enumValues.GetValue(0));
            }
            if (enumValues.Length >= 2)
            {
                return GetVisibilityDichotomy(value, enumValues.GetValue(1), enumValues.GetValue(0));
            }
            else if (enumValues.Length >= 1)
            {
                return GetVisibilityDichotomy(value, enumValues.GetValue(0), null);
            }

            return null;
        }

        if (args.TargetType == typeof(bool))
        {
            return GetVisibilityDichotomy(value, true, false);
        }

        if (args.TargetType == typeof(short))
        {
            return GetVisibilityTrichotomy<short>(value, 2, 1, 0);
        }
        if (args.TargetType == typeof(int))
        {
            return GetVisibilityTrichotomy(value, 2, 1, 0);
        }
        if (args.TargetType == typeof(long))
        {
            return GetVisibilityTrichotomy<long>(value, 2, 1, 0);
        }
        if (args.TargetType == typeof(float))
        {
            return GetVisibilityTrichotomy<float>(value, 2, 1, 0);
        }
        if (args.TargetType == typeof(double))
        {
            return GetVisibilityTrichotomy<double>(value, 2, 1, 0);
        }
        if (args.TargetType == typeof(decimal))
        {
            return GetVisibilityTrichotomy<decimal>(value, 2, 1, 0);
        }

        return null;
    }
}