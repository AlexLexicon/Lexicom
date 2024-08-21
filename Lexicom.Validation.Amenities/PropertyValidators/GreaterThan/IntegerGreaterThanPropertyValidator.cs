using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class IntegerGreaterThanValidator
{
    public static bool IsValid(int value, int valueToCompare)
    {
        return value > valueToCompare;
    }
}
public class IntegerGreaterThanPropertyValidator<T> : AbstractComparisonPropertyValidator<T, int>
{
    public const string NAME = nameof(IntegerGreaterThanPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be greater than {ComparisonValue}.";

    public IntegerGreaterThanPropertyValidator(long valueToCompare) : base((int)valueToCompare)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public IntegerGreaterThanPropertyValidator(Func<long> valueToCompareFunc) : base(() => (int)valueToCompareFunc.Invoke())
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public IntegerGreaterThanPropertyValidator(Func<T, long> valueToCompareFunc) : base(t => (int)valueToCompareFunc.Invoke(t))
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.GreaterThan;

    public override bool IsValid(int value, int valueToCompare)
    {
        return IntegerGreaterThanValidator.IsValid(value, valueToCompare);
    }
}