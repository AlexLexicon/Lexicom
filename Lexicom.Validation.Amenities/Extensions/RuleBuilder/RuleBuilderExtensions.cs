using FluentValidation;
using FluentValidation.Validators;
using Lexicom.Validation.Amenities.PropertyValidators;
using System.Diagnostics;

namespace Lexicom.Validation.Amenities.Extensions;
public static class RuleBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> Alphanumeric<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<AlphanumericPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> AnyDigits<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<AnyDigitsPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> AnyLetters<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<AnyLettersPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> AnyLowerCaseCharacters<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<AnyLowerCaseCharactersPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> AnyNonAlphanumeric<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<AnyNonAlphanumericPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> AnyUpperCaseCharacters<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<AnyUpperCaseCharactersPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> Digits<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<DigitsPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> DirectoryExists<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<DirectoryExistsPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> DirectoryPath<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<DirectoryPathPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> FileExists<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<FileExistsPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> FilePath<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<FilePathPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, TProperty> Guid<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) => ruleBuilder.SetPropertyValidator<GuidPropertyValidator<T, TProperty>, T, TProperty>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> Letters<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<LettersPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> NotAllDigits<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<NotAllDigitsPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> NotAllWhitespaces<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<NotAllWhiteSpacesPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> NotAnyDigits<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<NotAnyDigitsPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> NotAnyWhiteSpace<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<NotAnyWhiteSpacePropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> NotEscapedCharacters<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<NotEscapedCharactersPropertyValidator<T>, T, string?>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, TProperty> NotSimplyEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) => ruleBuilder.SetPropertyValidator<NotSimplyEmptyPropertyValidator<T, TProperty>, T, TProperty>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, TProperty> SimplyEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) => ruleBuilder.SetPropertyValidator<SimplyEmptyPropertyValidator<T, TProperty>, T, TProperty>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilder<T, string?> Length<T>(this IRuleBuilder<T, string?> ruleBuilder, int? minimum, int? maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        if (minimum is not null && minimum.Value == int.MinValue)
        {
            minimum = null;
        }

        if (maximum is not null && maximum.Value == int.MaxValue)
        {
            maximum = null;
        }

        if (minimum is null && maximum is null)
        {
            return ruleBuilder;
        }

        if (minimum is null)
        {
            if (maximum is null)
            {
                throw new UnreachableException($"{nameof(maximum)} can never be null at this point.");
            }

            return DefaultValidatorExtensions.MaximumLength(ruleBuilder, maximumLength: maximum.Value);
        }

        if (maximum is null)
        {
            if (minimum == 1)
            {
                //if we are saying the length must be at least 1 its the same as being simply not empty
                //which is a better message to use rather than length
                //if the simply not empty is already included, duplicate error messages are removed
                //by the standardized call that is usually called automatically
                return ruleBuilder.NotSimplyEmpty();
            }

            return DefaultValidatorExtensions.MinimumLength(ruleBuilder, minimumLength: minimum.Value);
        }

        if (minimum == maximum)
        {
            return DefaultValidatorExtensions.Length(ruleBuilder, exactLength: minimum.Value);
        }

        return DefaultValidatorExtensions.Length(ruleBuilder, min: minimum.Value, max: maximum.Value);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, TProperty> SetPropertyValidator<TValidator, T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) where TValidator : IPropertyValidator<T, TProperty>, new()
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new TValidator());
    }
}
