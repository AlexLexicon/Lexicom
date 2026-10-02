using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities;

public static class AbstractPropertyValidator
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static string GetLocalizedOrDefaultMessageTemplate(IDefaultMessagePropertyValidator validator, string? localizedMessageTemplate)
    {
        ArgumentNullException.ThrowIfNull(validator);

        if (!string.IsNullOrWhiteSpace(localizedMessageTemplate))
        {
            return localizedMessageTemplate;
        }

        return validator.DefaultMessageTemplate ?? throw new InvalidOperationException($"The validator type '{validator?.GetType().Name ?? "null"}' has a null '{nameof(validator.DefaultMessageTemplate)}'.");
    }
}
public abstract class AbstractPropertyValidator<T, TProperty> : PropertyValidator<T, TProperty>, IDefaultMessagePropertyValidator
{
    public abstract string DefaultMessageTemplate { get; }

    protected override string GetDefaultMessageTemplate(string? errorCode)
    {
        string localizedMessageTemplate = Localized(errorCode, Name);

        return AbstractPropertyValidator.GetLocalizedOrDefaultMessageTemplate(this, localizedMessageTemplate);
    }
}