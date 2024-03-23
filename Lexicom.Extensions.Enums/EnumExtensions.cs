using System.ComponentModel;
using System.Reflection;

namespace Lexicom.Extensions.Enums;
public static class EnumExtensions
{
    public static string? GetDescription(this Enum? value)
    {
        if (value is null)
        {
            return null;
        }

        string valueString = value.ToString();

        FieldInfo? fieldInfo = value
            .GetType()
            .GetField(valueString);

        if (fieldInfo is null)
        {
            return null;
        }

        DescriptionAttribute[]? attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false) as DescriptionAttribute[];

        DescriptionAttribute? descriptionAttribute = attributes?.FirstOrDefault();

        if (descriptionAttribute is not null)
        {
            return descriptionAttribute.Description;
        }

        return value.ToString();
    }

    /// <exception cref="ArgumentNullException"/>
    public static NotImplementedException ToNotImplementedException(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new NotImplementedException($"This operation using the enum '{value.GetType().FullName}' did not implement the value '{value}'.");
    }

    /// <exception cref="ArgumentNullException"/>
    public static NotSupportedException ToNotSupportedException(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new NotSupportedException($"This operation using the enum '{value.GetType().FullName}' does not support the value '{value}'.");
    }
}
