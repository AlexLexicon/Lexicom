using FluentValidation;
using Lexicom.Validation.Amenities.PropertyValidators;

namespace Lexicom.Validation.Amenities.Extensions;
public static class LessThanRuleBuilderExtensions
{
    /*
     * string?
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(() => minimumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(t => minimumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(minimumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThan<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new StringLessThanPropertyValidator<T>(minimumFunc));
    }

    /*
     * int
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(() => minimumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(t => minimumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(minimumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThan<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new IntegerLessThanPropertyValidator<T>(minimumFunc));
    }

    /*
     * long
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(() => minimumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(t => minimumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(minimumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThan<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LongLessThanPropertyValidator<T>(minimumFunc));
    }
}
