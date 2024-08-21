using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class LongGreaterThanValidator
{
    public static bool IsValid(long value, long valueToCompare)
    {
        return value > valueToCompare;
    }
}
public class LongGreaterThanPropertyValidator<T> : AbstractComparisonPropertyValidator<T, long>
{
    public const string NAME = nameof(LongGreaterThanPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be greater than {ComparisonValue}.";

    public LongGreaterThanPropertyValidator(long valueToCompare) : base(valueToCompare)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public LongGreaterThanPropertyValidator(Func<long> valueToCompareFunc) : base(valueToCompareFunc)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public LongGreaterThanPropertyValidator(Func<T, long> valueToCompareFunc) : base(valueToCompareFunc)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.GreaterThan;

    public override bool IsValid(long value, long valueToCompare)
    {
        return LongGreaterThanValidator.IsValid(value, valueToCompare);
    }
}