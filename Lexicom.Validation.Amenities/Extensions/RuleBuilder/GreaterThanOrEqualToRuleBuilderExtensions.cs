using FluentValidation;
using Lexicom.Validation.Amenities.PropertyValidators;

namespace Lexicom.Validation.Amenities.Extensions;

public static class GreaterThanOrEqualToRuleBuilderExtensions
{
    /*
     * string?
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(() => minimumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(t => minimumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(minimumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(minimumDelegate));
    }

    /*
     * int
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(() => minimumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(t => minimumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(minimumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(minimumDelegate));
    }

    /*
     * long
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(() => minimumDelegate.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, int> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(t => minimumDelegate.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(minimumDelegate));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, long> minimumDelegate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumDelegate);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(minimumDelegate));
    }
}
