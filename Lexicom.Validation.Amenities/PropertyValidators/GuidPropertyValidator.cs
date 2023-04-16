using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public class GuidPropertyValidator<T, TProperty> : AbstractPropertyValidator<T, TProperty>
{ 
    public const string NAME = nameof(GuidPropertyValidator<T, TProperty>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be a Guid.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (value is null)
        {
            return true;
        }

        if (value is Guid)
        {
            return true;
        }

        if (value is string str && Guid.TryParse(str, out Guid _))
        {
            return true;
        }

        return false;
    }
}