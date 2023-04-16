using System.Globalization;

namespace Lexicom.Wpf.ValueConverters.Abstractions;
public class ValueConverterArgs
{
    public required object? RawParameter { get; init; }
    public required Type TargetType { get; init; }
    public required CultureInfo Culture { get; init; }
}
