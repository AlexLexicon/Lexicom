using FluentValidation;

namespace Lexicom.Validation.Amenities.Extensions;
public static class EqualRuleBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, TProperty> Equal<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<TProperty> toCompareFunc, IEqualityComparer<TProperty>? comparer = null)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(toCompareFunc);

        return ruleBuilder.SetValidator(new FluentValidation.Validators.EqualValidator<T, TProperty>(_ => toCompareFunc.Invoke(), member: null, memberDisplayName: null, comparer));
    }
    /// <exception cref="ArgumentNullException"/>
    public static IRuleBuilderOptions<T, TProperty> Equal<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, TProperty> toCompareFunc, IEqualityComparer<TProperty>? comparer = null)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(toCompareFunc);

        return ruleBuilder.SetValidator(new FluentValidation.Validators.EqualValidator<T, TProperty>(toCompareFunc, member: null, memberDisplayName: null, comparer));
    }
}
