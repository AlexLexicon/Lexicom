using FluentValidation.Resources;
using FluentValidation.Validators;

namespace Lexicom.Validation.Extensions;

public static class LanguageManagerExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static LanguageManager AddLexicomTranslations(this LanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(languageManager);

        //standardize validation messages
#pragma warning disable CS0618 // Type or member is obsolete
        languageManager.AddEnTranslation(nameof(EmailValidator<>), "'{PropertyName}' must be a valid email address.");
#pragma warning restore CS0618 // Type or member is obsolete
        languageManager.AddEnTranslation(nameof(GreaterThanOrEqualValidator<,>), "'{PropertyName}' must be greater than or equal to {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(GreaterThanValidator<,>), "'{PropertyName}' must be greater than {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(LengthValidator<>), "'{PropertyName}' must have a length between {MinLength} and {MaxLength} characters but was {TotalLength}.");
        languageManager.AddEnTranslation(nameof(MinimumLengthValidator<>), "'{PropertyName}' must have a length of at least {MinLength} characters but was {TotalLength}.");
        languageManager.AddEnTranslation(nameof(MaximumLengthValidator<>), "'{PropertyName}' must have a length of {MaxLength} or fewer characters but was {TotalLength}.");
        languageManager.AddEnTranslation(nameof(LessThanOrEqualValidator<,>), "'{PropertyName}' must be less than or equal to {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(LessThanValidator<,>), "'{PropertyName}' must be less than {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(NotEmptyValidator<,>), "The '{PropertyName}' field is required.");
        languageManager.AddEnTranslation(nameof(NotEqualValidator<,>), "'{PropertyName}' must not be equal to {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(NotNullValidator<,>), "The '{PropertyName}' field is required.");
        languageManager.AddEnTranslation(nameof(PredicateValidator<,>), "The '{PropertyName}' condition is required.");
        languageManager.AddEnTranslation(nameof(AsyncPredicateValidator<,>), "The '{PropertyName}' condition is required.");
        languageManager.AddEnTranslation(nameof(RegularExpressionValidator<>), "The '{PropertyName}' is not in the correct format.");
        languageManager.AddEnTranslation(nameof(EqualValidator<,>), "'{PropertyName}' must be equal to {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(ExactLengthValidator<>), "'{PropertyName}' must have a length of {MaxLength} characters but was {TotalLength}.");
        languageManager.AddEnTranslation(nameof(InclusiveBetweenValidator<,>), "'{PropertyName}' must be between {From} and {To} but was {PropertyValue}.");
        languageManager.AddEnTranslation(nameof(ExclusiveBetweenValidator<,>), "'{PropertyName}' must be between {From} and {To} (exclusive) but was {PropertyValue}.");
        languageManager.AddEnTranslation(nameof(CreditCardValidator<>), "'{PropertyName}' must be a valid credit card number.");
        languageManager.AddEnTranslation(nameof(EmptyValidator<,>), "The '{PropertyName}' field must not be provided.");
        languageManager.AddEnTranslation(nameof(NullValidator<,>), "The '{PropertyName}' field must not be provided.");
        languageManager.AddEnTranslation(nameof(EnumValidator<,>), "'{PropertyName}' must be in a range of values which does not include '{PropertyValue}'.");

        languageManager.AddEnTranslation("Length_Simple", "'{PropertyName}' must have a length between {MinLength} and {MaxLength} characters.");
        languageManager.AddEnTranslation("MinimumLength_Simple", "'{PropertyName}' must have a length of at least {MinLength} characters.");
        languageManager.AddEnTranslation("MaximumLength_Simple", "'{PropertyName}' must have a length of {MaxLength} or fewer characters.");
        languageManager.AddEnTranslation("ExactLength_Simple", "'{PropertyName}' must have a length of {MaxLength} characters.");
        languageManager.AddEnTranslation("InclusiveBetween_Simple", "'{PropertyName}' must be between {From} and {To}.");

        return languageManager;
    }

    /// <exception cref="ArgumentNullException"/>
    public static LanguageManager AddEnTranslation(this LanguageManager languageManager, string key, string message)
    {
        ArgumentNullException.ThrowIfNull(languageManager);
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(message);

        languageManager.AddTranslation("en", key, message);
        languageManager.AddTranslation("en-US", key, message);
        languageManager.AddTranslation("en-GB", key, message);

        return languageManager;
    }
}
