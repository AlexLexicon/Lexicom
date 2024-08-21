using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class IntegerGreaterThanOrEqualToValidator
{
    public static bool IsValid(int value, int valueToCompare)
    {
        return value >= valueToCompare;
    }
}
public class IntegerGreaterThanOrEqualToPropertyValidator<T> : AbstractComparisonPropertyValidator<T, int>
{
    public const string NAME = nameof(IntegerGreaterThanOrEqualToPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be greater than or equal to {ComparisonValue}.";

    public IntegerGreaterThanOrEqualToPropertyValidator(long valueToCompare) : base((int)valueToCompare)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public IntegerGreaterThanOrEqualToPropertyValidator(Func<long> valueToCompareFunc) : base(() => (int)valueToCompareFunc.Invoke())
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public IntegerGreaterThanOrEqualToPropertyValidator(Func<T, long> valueToCompareFunc) : base(t => (int)valueToCompareFunc.Invoke(t))
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.GreaterThanOrEqual;

    public override bool IsValid(int value, int valueToCompare)
    {
        return IntegerGreaterThanOrEqualToValidator.IsValid(value, valueToCompare);
    }
}