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

        languageManager.AddEnTranslation(IntegerGreaterThanPropertyValidator<object>.NAME, IntegerGreaterThanPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(LongGreaterThanPropertyValidator<object>.NAME, LongGreaterThanPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(StringGreaterThanPropertyValidator<object>.NAME, StringGreaterThanPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);

        languageManager.AddEnTranslation(IntegerGreaterThanOrEqualToPropertyValidator<object>.NAME, IntegerGreaterThanOrEqualToPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(LongGreaterThanOrEqualToPropertyValidator<object>.NAME, LongGreaterThanOrEqualToPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(StringGreaterThanOrEqualToPropertyValidator<object>.NAME, StringGreaterThanOrEqualToPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);

        languageManager.AddEnTranslation(IntegerLessThanPropertyValidator<object>.NAME, IntegerLessThanPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(LongLessThanPropertyValidator<object>.NAME, LongLessThanPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(StringLessThanPropertyValidator<object>.NAME, StringLessThanPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);

        languageManager.AddEnTranslation(IntegerLessThanOrEqualToPropertyValidator<object>.NAME, IntegerLessThanOrEqualToPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(LongLessThanOrEqualToPropertyValidator<object>.NAME, LongLessThanOrEqualToPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(StringLessThanOrEqualToPropertyValidator<object>.NAME, StringLessThanOrEqualToPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);

        languageManager.AddEnTranslation(AlphanumericPropertyValidator<object>.NAME, AlphanumericPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyDigitsPropertyValidator<object>.NAME, AnyDigitsPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyLettersPropertyValidator<object>.NAME, AnyLettersPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyLowerCaseCharactersPropertyValidator<object>.NAME, AnyLowerCaseCharactersPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyNonAlphanumericPropertyValidator<object>.NAME, AnyNonAlphanumericPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(AnyUpperCaseCharactersPropertyValidator<object>.NAME, AnyUpperCaseCharactersPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(DigitsPropertyValidator<object>.NAME, DigitsPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(DirectoryExistsPropertyValidator<object>.NAME, DirectoryExistsPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(DirectoryPathPropertyValidator<object>.NAME, DirectoryPathPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(FileExistsPropertyValidator<object>.NAME, FileExistsPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(FilePathPropertyValidator<object>.NAME, FilePathPropertyValidator<object>.DEFAULT_MESSAGE_TEMPLATE);
        languageManager.AddEnTranslation(GuidPropertyValidator<object, object>.NAME, GuidPropertyValidator<object, object>.DEFAULT_MESSAGE_TEMPLATE);
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
