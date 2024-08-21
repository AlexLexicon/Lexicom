using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class IntegerLessThanOrEqualToValidator
{
    public static bool IsValid(int value, int valueToCompare)
    {
        return value <= valueToCompare;
    }
}
public class IntegerLessThanOrEqualToPropertyValidator<T> : AbstractComparisonPropertyValidator<T, int>
{
    public const string NAME = nameof(IntegerLessThanOrEqualToPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be less than or equal to {ComparisonValue}.";

    public IntegerLessThanOrEqualToPropertyValidator(long valueToCompare) : base((int)valueToCompare)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public IntegerLessThanOrEqualToPropertyValidator(Func<long> valueToCompareFunc) : base(() => (int)valueToCompareFunc.Invoke())
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public IntegerLessThanOrEqualToPropertyValidator(Func<T, long> valueToCompareFunc) : base(t => (int)valueToCompareFunc.Invoke(t))
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.LessThanOrEqual;

    public override bool IsValid(int value, int valueToCompare)
    {
        return IntegerLessThanOrEqualToValidator.IsValid(value, valueToCompare);
    }
}