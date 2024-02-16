using FluentValidation.Validators;
using FluentValidation;
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
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new GreaterThan<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new GreaterThan<T>(t => maximumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new GreaterThan<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new GreaterThan<T>(maximumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new GreaterThanOrEqualTo<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new GreaterThanOrEqualTo<T>(t => maximumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new GreaterThanOrEqualTo<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new GreaterThanOrEqualTo<T>(maximumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, TProperty> Guid<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) => ruleBuilder.SetPropertyValidator<GuidPropertyValidator<T, TProperty>, T, TProperty>();
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LessThan<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LessThan<T>(t => minimumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LessThan<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LessThan<T>(minimumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LessThanOrEqualTo<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LessThanOrEqualTo<T>(t => minimumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LessThanOrEqualTo<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LessThanOrEqualTo<T>(minimumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> Letters<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.SetPropertyValidator<LettersPropertyValidator<T>, T, string?>();
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
    public static IRuleBuilderOptions<T, TProperty> SetPropertyValidator<TValidator, T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) where TValidator : IPropertyValidator<T, TProperty>, new()
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new TValidator());
    }

    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilder<T, string?> Length<T>(this IRuleBuilder<T, string?> ruleBuilder, int? min, int? max)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        if (min is not null && min.Value == int.MinValue)
        {
            min = null;
        }

        if (max is not null && max.Value == int.MaxValue)
        {
            max = null;
        }

        if (min is null && max is null)
        {
            return ruleBuilder;
        }

        if (min is null)
        {
            if (max is null)
            {
                throw new UnreachableException($"{nameof(max)} can never be null at this point.");
            }

            return DefaultValidatorExtensions.MaximumLength(ruleBuilder, maximumLength: max.Value);
        }

        if (max is null)
        {
            if (min == 1)
            {
                //if we are saying the length must be at least 1 its the same as being simply not empty
                //which is a better message to use rather than length
                //if the simply not empty is already included, duplicate error messages are removed
                //by the standardized call that is usually called automatically
                return ruleBuilder.NotSimplyEmpty();
            }

            return DefaultValidatorExtensions.MinimumLength(ruleBuilder, minimumLength: min.Value);
        }

        if (min == max)
        {
            return DefaultValidatorExtensions.Length(ruleBuilder, exactLength: min.Value);
        }

        return DefaultValidatorExtensions.Length(ruleBuilder, min: min.Value, max: max.Value);
    }
}
