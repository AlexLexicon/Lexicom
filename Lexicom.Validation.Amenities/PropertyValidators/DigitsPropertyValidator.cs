using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class DigitsValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return value.All(c => char.IsDigit(c) || char.IsWhiteSpace(c));
    }
}
public class DigitsPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{ 
    public const string NAME = nameof(DigitsPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must contain only digits.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return DigitsValidator.IsValid(value);
    }
}