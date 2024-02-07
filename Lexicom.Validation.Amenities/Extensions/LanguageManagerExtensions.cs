using FluentValidation.Resources;
using Lexicom.Validation.Amenities.PropertyValidators;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.Amenities.Extensions;
public static class LanguageManagerExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static LanguageManager AddLexicomAmenitiesTranslations(this LanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(languageManager);

        languageManager.AddEnTranslation(AlphanumericPropertyValidator<object>.NAME, AlphanumericPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyDigitsPropertyValidator<object>.NAME, AnyDigitsPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyLettersPropertyValidator<object>.NAME, AnyLettersPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyLowerCaseCharactersPropertyValidator<object>.NAME, AnyLowerCaseCharactersPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyNonAlphanumericPropertyValidator<object>.NAME, AnyNonAlphanumericPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyUpperCaseCharactersPropertyValidator<object>.NAME, AnyUpperCaseCharactersPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(DigitsPropertyValidator<object>.NAME, DigitsPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(GreaterThan<object>.NAME, GreaterThan<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(GreaterThanOrEqualTo<object>.NAME, GreaterThanOrEqualTo<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(GuidPropertyValidator<object, object>.NAME, GuidPropertyValidator<object, object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(LessThan<object>.NAME, LessThan<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(LessThanOrEqualTo<object>.NAME, LessThanOrEqualTo<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(LettersPropertyValidator<object>.NAME, LettersPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(NotAllWhiteSpacesPropertyValidator<object>.NAME, NotAllWhiteSpacesPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(NotAnyDigitsPropertyValidator<object>.NAME, NotAnyDigitsPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(NotAnyWhiteSpacePropertyValidator<object>.NAME, NotAnyWhiteSpacePropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(NotEscapedCharactersPropertyValidator<object>.NAME, NotEscapedCharactersPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(NotSimplyEmptyPropertyValidator<object, object>.NAME, NotSimplyEmptyPropertyValidator<object, object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(SimplyEmptyPropertyValidator<object, object>.NAME, SimplyEmptyPropertyValidator<object, object>.DEFAULT_MESSAGE_TEMPLATE);

        return languageManager;
    }
}
