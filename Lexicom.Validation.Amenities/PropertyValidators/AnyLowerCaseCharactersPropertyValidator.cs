using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class AnyLowerCaseCharactersValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return value.Any(char.IsLower);
    }
}
public class AnyLowerCaseCharactersPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(AnyLowerCaseCharactersPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must contain any lower case character.";

    public override string Name => NAME;
    public override string DefaultMessageTemplate => DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return AnyLowerCaseCharactersValidator.IsValid(value);
    }
}
