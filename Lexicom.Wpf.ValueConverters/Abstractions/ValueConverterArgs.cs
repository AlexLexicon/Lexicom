using System.Globalization;

namespace Lexicom.Wpf.ValueConverters.Abstractions;
public class ValueConverterArgs
{
    public ValueConverterArgs(
        Type targetType, 
        CultureInfo culture)
    {
        TargetType = targetType;
        Culture = culture;
    }

    public Type TargetType { get; }
    public CultureInfo Culture { get; }
    public object? RawParameter { get; init; }
}
