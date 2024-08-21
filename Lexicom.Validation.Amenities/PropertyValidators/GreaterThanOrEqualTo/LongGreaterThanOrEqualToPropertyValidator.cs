using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class LongGreaterThanOrEqualToValidator
{
    public static bool IsValid(long value, long valueToCompare)
    {
        return value >= valueToCompare;
    }
}
public class LongGreaterThanOrEqualToPropertyValidator<T> : AbstractComparisonPropertyValidator<T, long>
{
    public const string NAME = nameof(LongGreaterThanOrEqualToPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be greater than or equal to {ComparisonValue}.";

    public LongGreaterThanOrEqualToPropertyValidator(long valueToCompare) : base(valueToCompare)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public LongGreaterThanOrEqualToPropertyValidator(Func<long> valueToCompareFunc) : base(valueToCompareFunc)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public LongGreaterThanOrEqualToPropertyValidator(Func<T, long> valueToCompareFunc) : base(valueToCompareFunc)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.GreaterThanOrEqual;

    public override bool IsValid(long value, long valueToCompare)
    {
        return LongGreaterThanOrEqualToValidator.IsValid(value, valueToCompare);
    }
}