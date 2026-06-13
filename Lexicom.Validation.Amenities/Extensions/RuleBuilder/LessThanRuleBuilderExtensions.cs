using FluentValidation;
using Lexicom.Validation.Amenities.PropertyValidators;

namespace Lexicom.Validation.Amenities.Extensions;

public static class LessThanRuleBuilderExtensions
{
    /*
     * string?
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(() => maximumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(t => maximumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(maximumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(maximumDelegate));
    }

    /*
     * int
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(() => maximumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(t => maximumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(maximumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(maximumDelegate));
    }

    /*
     * long
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(() => maximumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, int> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(t => maximumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(maximumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, long> maximumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumDelegate);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(maximumDelegate));
    }
}
