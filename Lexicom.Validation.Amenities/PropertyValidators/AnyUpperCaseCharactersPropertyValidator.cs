using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class AnyUpperCaseCharactersValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return value.Any(char.IsUpper);
    }
}
public class AnyUpperCaseCharactersPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(AnyUpperCaseCharactersPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must contain any upper case character.";

    public override string Name => NAME;
    public override string DefaultMessageTemplate => DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return AnyUpperCaseCharactersValidator.IsValid(value);
    }
}
