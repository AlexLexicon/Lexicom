using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities;
public abstract class AbstractPropertyValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public abstract string DefaultMessageTemplate { get; }

    protected override string GetDefaultMessageTemplate(string? errorCode)
    {
        string? localizedMessageTemplate = Localized(errorCode, Name);

        if (!string.IsNullOrWhiteSpace(localizedMessageTemplate))
        {
            return localizedMessageTemplate;
        }

        return DefaultMessageTemplate ?? throw new NullReferenceException($"{nameof(DefaultMessageTemplate)} was null");
    }
}