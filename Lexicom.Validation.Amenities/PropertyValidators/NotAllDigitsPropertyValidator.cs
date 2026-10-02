using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;

public static class NotAllDigitsValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return !value.All(char.IsDigit);
    }
}
public class NotAllDigitsPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(NotAllDigitsPropertyValidator<>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must not contain only digits.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return NotAllDigitsValidator.IsValid(value);
    }
}