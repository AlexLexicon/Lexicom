using FluentValidation;
using Lexicom.Validation.Amenities.PropertyValidators;

namespace Lexicom.Validation.Amenities.Extensions;

public static class LessThanOrEqualToRuleBuilderExtensions
{
    /*
     * string?
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(() => maximumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(t => maximumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(maximumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(maximumDelegate));
    }

    /*
     * int
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(() => maximumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(t => maximumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(maximumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(maximumDelegate));
    }

    /*
     * long
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(() => maximumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(t => maximumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(maximumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(maximumDelegate));
    }
}
