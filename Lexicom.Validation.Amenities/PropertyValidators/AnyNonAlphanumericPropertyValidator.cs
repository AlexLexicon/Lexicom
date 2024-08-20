using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class AnyNonAlphanumericValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return value.Any(c => !char.IsLetter(c) && !char.IsDigit(c));
    }
}
public class AnyNonAlphanumericPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(AnyNonAlphanumericPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must contain any non-alphanumeric character.";

    public override string Name => NAME;
    public override string DefaultMessageTemplate => DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return AnyNonAlphanumericValidator.IsValid(value);
    }
}
