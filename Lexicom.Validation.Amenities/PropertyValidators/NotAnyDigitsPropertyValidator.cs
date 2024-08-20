using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class NotAnyDigitsValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return !value.Any(char.IsDigit);
    }
}
public class NotAnyDigitsPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(NotAnyDigitsPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must not contain any digits.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return NotAnyDigitsValidator.IsValid(value);
    }
}