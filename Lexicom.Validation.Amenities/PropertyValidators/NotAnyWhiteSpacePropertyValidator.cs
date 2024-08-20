using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class NotAnyWhiteSpacePropertyValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return !value.Any(char.IsWhiteSpace);
    }
}
public class NotAnyWhiteSpacePropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(NotAnyWhiteSpacePropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must not contain any white space characters.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return NotAnyWhiteSpacePropertyValidator.IsValid(value);
    }
}