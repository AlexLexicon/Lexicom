namespace Lexicom.Validation.Amenities.Extensions;

public static class DefaultMessagePropertyValidatorExtensions
{
    public static string GetLocalizedOrDefaultMessageTemplate(this IDefaultMessagePropertyValidator validator, string? errorCode)
    {
        string? localizedMessageTemplate = validator.Localized(errorCode, validator.Name);

        if (!string.IsNullOrWhiteSpace(localizedMessageTemplate))
        {
            return localizedMessageTemplate;
        }

        return validator.DefaultMessageTemplate ?? throw new InvalidOperationException($"The validator type '{validator?.GetType().Name ?? "null"}' has a null '{nameof(validator.DefaultMessageTemplate)}'.");
    }
}
