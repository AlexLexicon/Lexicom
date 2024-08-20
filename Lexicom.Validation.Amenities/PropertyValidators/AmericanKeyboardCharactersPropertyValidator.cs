using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class AmericanKeyboardCharactersValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return value.All(c => char.IsDigit(c) || char.IsLetter(c) || char.IsWhiteSpace(c) || Constants.AMERICAN_SYMBOLS.Contains(c));
    }
}
public class AmericanKeyboardCharactersPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(AmericanKeyboardCharactersPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must contain only characters that exist on an american keyboard.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return AmericanKeyboardCharactersValidator.IsValid(value);
    }
}