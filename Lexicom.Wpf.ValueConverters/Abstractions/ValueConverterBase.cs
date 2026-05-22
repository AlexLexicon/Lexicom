using Lexicom.Wpf.ValueConverters.Exceptions;
using System.Globalization;
using System.Windows.Data;

namespace Lexicom.Wpf.ValueConverters.Abstractions;

public abstract class ValueConverterBase<T> : ValueConverterBase
{
    protected override object? Convert(object? value, ValueConverterArgs args)
    {
        return TConvert(value, args);
    }
    protected override object? ConvertBack(object? value, ValueConverterArgs args)
    {
        if (value is null)
        {
            return TConvertBack(default, args);
        }

        if (value is T tValue)
        {
            return TConvertBack(tValue, args);
        }

        throw new NotConvertableException();
    }
    protected abstract T? TConvert(object? value, ValueConverterArgs args);
    protected abstract object? TConvertBack(T? value, ValueConverterArgs args);
}
public abstract class ValueConverterBase : IValueConverter
{
    public const char PARAMETER_SPLIT_SECTION = '_';
    public const char PARAMETER_SPLIT_KEY_VALUE = ':';

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => HandleConvert(value, targetType, parameter, culture, Convert);
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => HandleConvert(value, targetType, parameter, culture, ConvertBack);
    private object? HandleConvert(object? value, Type targetType, object? parameter, CultureInfo culture, Func<object?, ValueConverterArgs, object?> convertDelegate)
    {
        return convertDelegate.Invoke(value, new ValueConverterArgs
        {
            RawParameter = parameter,
            TargetType = targetType,
            Culture = culture,
            Parameters = ParseParameters(parameter),
        });
    }

    protected abstract object? Convert(object? value, ValueConverterArgs args);
    protected abstract object? ConvertBack(object? value, ValueConverterArgs args);

    /// <exception cref="ArgumentNullException"/>
    protected virtual bool HasParameter(ValueConverterArgs args, ValueConverterParameterDefinition valueConverterParameterDefinition)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(valueConverterParameterDefinition);

        return valueConverterParameterDefinition.Match(args.Parameters);
    }
    /// <exception cref="ArgumentNullException"/>
    protected virtual bool HasParameter<T>(ValueConverterArgs args, ValueConverterParameterDefinition<T> valueConverterParameterDefinition)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(valueConverterParameterDefinition);

        return valueConverterParameterDefinition.Match(args.Parameters);
    }
    /// <exception cref="ArgumentNullException"/>
    protected virtual bool HasParameter<T>(ValueConverterArgs args, ValueConverterParameterDefinition<T> valueConverterParameterDefinition, out T? value)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(valueConverterParameterDefinition);

        return valueConverterParameterDefinition.Match(args.Parameters, out value);
    }

    protected virtual IReadOnlyList<ValueConverterParameter> ParseParameters(object? parameter)
    {
        var parameters = new List<ValueConverterParameter>();

        string? parameterString = parameter?.ToString();

        if (parameterString is null)
        {
            return parameters;
        }

        string[] splitSections = parameterString.Split(PARAMETER_SPLIT_SECTION, StringSplitOptions.RemoveEmptyEntries);

        foreach (string splitSection in splitSections)
        {
            if (!string.IsNullOrWhiteSpace(splitSection))
            {
                string[] splitKeyValue = splitSection.Split(PARAMETER_SPLIT_KEY_VALUE, StringSplitOptions.RemoveEmptyEntries);

                if (splitKeyValue.Length > 0)
                {
                    string key = splitKeyValue[0];

                    string[] values;
                    if (splitKeyValue.Length > 1)
                    {
                        values = splitKeyValue
                            .Skip(1)
                            .ToArray();
                    }
                    else
                    {
                        values = [];
                    }

                    parameters.Add(new ValueConverterParameter
                    {
                        Key = key,
                        Values = values,
                    });
                }
            }
        }

        return parameters;
    }
}

