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
        languageManager.AddEnTranslation(nameof(EmailValidator<object>), "'{PropertyName}' must be a valid email address.");
#pragma warning restore CS0618 // Type or member is obsolete
        languageManager.AddEnTranslation(nameof(GreaterThanOrEqualValidator<object, int>), "'{PropertyName}' must be greater than or equal to {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(GreaterThanValidator<object, int>), "'{PropertyName}' must be greater than {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(LengthValidator<object>), "'{PropertyName}' must have a length between {MinLength} and {MaxLength} characters but was {TotalLength}.");
        languageManager.AddEnTranslation(nameof(MinimumLengthValidator<object>), "'{PropertyName}' must have a length of at least {MinLength} characters but was {TotalLength}.");
        languageManager.AddEnTranslation(nameof(MaximumLengthValidator<object>), "'{PropertyName}' must have a length of {MaxLength} or fewer characters but was {TotalLength}.");
        languageManager.AddEnTranslation(nameof(LessThanOrEqualValidator<object, int>), "'{PropertyName}' must be less than or equal to {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(LessThanValidator<object, int>), "'{PropertyName}' must be less than {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(NotEmptyValidator<object, object>), "The '{PropertyName}' field is required.");
        languageManager.AddEnTranslation(nameof(NotEqualValidator<object, object>), "'{PropertyName}' must not be equal to {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(NotNullValidator<object, object>), "The '{PropertyName}' field is required.");
        languageManager.AddEnTranslation(nameof(PredicateValidator<object, object>), "The '{PropertyName}' condition is required.");
        languageManager.AddEnTranslation(nameof(AsyncPredicateValidator<object, object>), "The '{PropertyName}' condition is required.");
        languageManager.AddEnTranslation(nameof(RegularExpressionValidator<object>), "The '{PropertyName}' is not in the correct format.");
        languageManager.AddEnTranslation(nameof(EqualValidator<object, object>), "'{PropertyName}' must be equal to {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(ExactLengthValidator<object>), "'{PropertyName}' must has a length of {MaxLength} characters but was {TotalLength}.");
        languageManager.AddEnTranslation(nameof(InclusiveBetweenValidator<object, object>), "'{PropertyName}' must be between {From} and {To} but was {PropertyValue}.");
        languageManager.AddEnTranslation(nameof(ExclusiveBetweenValidator<object, object>), "'{PropertyName}' must be between {From} and {To} (exclusive) but was {PropertyValue}.");
        languageManager.AddEnTranslation(nameof(CreditCardValidator<object>), "'{PropertyName}' must be a valid credit card number.");
        languageManager.AddEnTranslation(nameof(ScalePrecisionValidator<object>), "'{PropertyName}' must not have more than {ExpectedPrecision} digits in total, with allowance for {ExpectedScale} decimals but {Digits} digits with {ActualScale} decimals were found.");
        languageManager.AddEnTranslation(nameof(EmptyValidator<object, object>), "The '{PropertyName}' field must not be provided.");
        languageManager.AddEnTranslation(nameof(NullValidator<object, object>), "The '{PropertyName}' field must not be provided.");
        languageManager.AddEnTranslation(nameof(EnumValidator<object, object>), "'{PropertyName}' must be in a range of values which does not include '{PropertyValue}'.");

        languageManager.AddEnTranslation("Length_Simple", "'{PropertyName}' must have a length between {MinLength} and {MaxLength} characters.");
        languageManager.AddEnTranslation("MinimumLength_Simple", "'{PropertyName}' must have a length of at least {MinLength} characters.");
        languageManager.AddEnTranslation("MaximumLength_Simple", "'{PropertyName}' must have a length of {MaxLength} or fewer characters.");
        languageManager.AddEnTranslation("ExactLength_Simple", "'{PropertyName}' must has a length of {MaxLength} characters.");
        languageManager.AddEnTranslation("InclusiveBetween_Simple", "'{PropertyName}' must be between {From} and {To}.");

        return languageManager;
    }

    /// <exception cref="ArgumentNullException"/>
    public static LanguageManager AddEnTranslation(this LanguageManager languageManager, string key, string message)
    {
        ArgumentNullException.ThrowIfNull(languageManager);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(message);

        languageManager.AddTranslation("en", key, message);
        languageManager.AddTranslation("en-US", key, message);
        languageManager.AddTranslation("en-GB", key, message);

        return languageManager;
    }
}
