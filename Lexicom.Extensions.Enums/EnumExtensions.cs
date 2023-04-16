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

        FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());

        if (fieldInfo is null)
        {
            return null;
        }

        DescriptionAttribute[]? attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

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

        return new NotImplementedException($"An operation using the enum '{value.GetType().FullName}' did not implement the value '{value}'.");
    }
}
