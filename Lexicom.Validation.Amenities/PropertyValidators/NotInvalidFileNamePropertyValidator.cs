using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;

public static class NotInvalidFileNamePropertyValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

        return !value.Any(invalidFileNameChars.Contains);
    }
}
public class NotInvalidFileNamePropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(NotInvalidFileNamePropertyValidator<>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must not contain any invalid file name characters.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return NotInvalidFileNamePropertyValidator.IsValid(value);
    }
}
