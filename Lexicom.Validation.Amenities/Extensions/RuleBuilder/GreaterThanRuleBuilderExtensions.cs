using FluentValidation;
using Lexicom.Validation.Amenities.PropertyValidators;

namespace Lexicom.Validation.Amenities.Extensions;

public static class GreaterThanRuleBuilderExtensions
{
    /*
     * string?
     */

    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(() => minimumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(t => minimumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(minimumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(minimumDelegate));
    }

    /*
     * int
     */

    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(() => minimumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(t => minimumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(minimumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(minimumDelegate));
    }

    /*
     * long
     */

    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(() => minimumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(t => minimumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(minimumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(minimumDelegate));
    }
}