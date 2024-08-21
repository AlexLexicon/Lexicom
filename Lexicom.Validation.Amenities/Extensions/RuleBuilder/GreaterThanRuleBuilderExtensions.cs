using FluentValidation;
using Lexicom.Validation.Amenities.PropertyValidators;

namespace Lexicom.Validation.Amenities.Extensions;
public static class GreaterThanRuleBuilderExtensions
{
    /*
     * string?
     */

    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(() => maximumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(t => maximumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(maximumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> GreaterThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new StringGreaterThanPropertyValidator<T>(maximumFunc));
    }

    /*
     * int
     */

    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(() => maximumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(t => maximumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, long maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(maximumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> GreaterThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new IntegerGreaterThanPropertyValidator<T>(maximumFunc));
    }

    /*
     * long
     */

    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, int maximum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(maximum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(() => maximumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, int> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(t => maximumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(maximumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> GreaterThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<long> maximumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(maximumFunc);

        return ruleBuilder.SetValidator(new LongGreaterThanPropertyValidator<T>(maximumFunc));
    }
}