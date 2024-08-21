using FluentValidation;
using Lexicom.Validation.Amenities.PropertyValidators;

namespace Lexicom.Validation.Amenities.Extensions;
public static class GreaterThanOrEqualToRuleBuilderExtensions
{
    /*
     * string?
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(() => maximumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(t => maximumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(maximumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new StringGreaterThanOrEqualToPropertyValidator<T>(maximumFunc));
    }

    /*
     * int
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(() => maximumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(t => maximumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(maximumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new IntegerGreaterThanOrEqualToPropertyValidator<T>(maximumFunc));
    }

    /*
     * long
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(() => maximumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(t => maximumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(maximumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new LongGreaterThanOrEqualToPropertyValidator<T>(maximumFunc));
    }
}
