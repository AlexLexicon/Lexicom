using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public class NotAllWhiteSpacesPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(NotAllWhiteSpacesPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "The '{PropertyName}' field is required.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return !string.IsNullOrWhiteSpace(value);
    }
}