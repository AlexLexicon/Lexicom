using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class LongLessThanValidator
{
    public static bool IsValid(long value, long valueToCompare)
    {
        return value < valueToCompare;
    }
}
public class LongLessThanPropertyValidator<T> : AbstractComparisonPropertyValidator<T, long>
{
    public const string NAME = nameof(LongLessThanPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be less than {ComparisonValue}.";

    public LongLessThanPropertyValidator(long valueToCompare) : base(valueToCompare)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public LongLessThanPropertyValidator(Func<long> valueToCompareFunc) : base(valueToCompareFunc)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public LongLessThanPropertyValidator(Func<T, long> valueToCompareFunc) : base(valueToCompareFunc)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.LessThan;

    public override bool IsValid(long value, long valueToCompare)
    {
        return LongLessThanValidator.IsValid(value, valueToCompare);
    }
}