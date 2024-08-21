using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class LongLessThanOrEqualToValidator
{
    public static bool IsValid(long value, long valueToCompare)
    {
        return value <= valueToCompare;
    }
}
public class LongLessThanOrEqualToPropertyValidator<T> : AbstractComparisonPropertyValidator<T, long>
{
    public const string NAME = nameof(LongLessThanOrEqualToPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be less than or equal to {ComparisonValue}.";

    public LongLessThanOrEqualToPropertyValidator(long valueToCompare) : base(valueToCompare)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public LongLessThanOrEqualToPropertyValidator(Func<long> valueToCompareFunc) : base(valueToCompareFunc)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public LongLessThanOrEqualToPropertyValidator(Func<T, long> valueToCompareFunc) : base(valueToCompareFunc)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.LessThanOrEqual;

    public override bool IsValid(long value, long valueToCompare)
    {
        return LongLessThanOrEqualToValidator.IsValid(value, valueToCompare);
    }
}