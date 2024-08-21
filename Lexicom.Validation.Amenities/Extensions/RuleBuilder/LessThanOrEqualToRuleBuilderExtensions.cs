using FluentValidation;
using Lexicom.Validation.Amenities.PropertyValidators;

namespace Lexicom.Validation.Amenities.Extensions;
public static class LessThanOrEqualToRuleBuilderExtensions
{
    /*
     * string?
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(() => minimumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(t => minimumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(minimumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, string?> LessThanOrEqualTo<T>(this IRuleBuilder<T, string?> ruleBuilder, Func<T, long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new StringLessThanOrEqualToPropertyValidator<T>(minimumFunc));
    }

    /*
     * int
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(() => minimumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(t => minimumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, long minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(minimumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, int> LessThanOrEqualTo<T>(this IRuleBuilder<T, int> ruleBuilder, Func<T, long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new IntegerLessThanOrEqualToPropertyValidator<T>(minimumFunc));
    }

    /*
     * long
     */
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, int minimum)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(minimum));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(() => minimumFunc.Invoke()));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, int> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(t => minimumFunc.Invoke(t)));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(minimumFunc));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, long> LessThanOrEqualTo<T>(this IRuleBuilder<T, long> ruleBuilder, Func<T, long> minimumFunc)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(minimumFunc);

        return ruleBuilder.SetValidator(new LongLessThanOrEqualToPropertyValidator<T>(minimumFunc));
    }
}
