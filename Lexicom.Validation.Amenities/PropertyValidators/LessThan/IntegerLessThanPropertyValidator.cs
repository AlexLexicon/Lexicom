using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class IntegerLessThanValidator
{
    public static bool IsValid(int value, int valueToCompare)
    {
        return value < valueToCompare;
    }
}
public class IntegerLessThanPropertyValidator<T> : AbstractComparisonPropertyValidator<T, int>
{
    public const string NAME = nameof(IntegerLessThanPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be less than {ComparisonValue}.";

    public IntegerLessThanPropertyValidator(long valueToCompare) : base((int)valueToCompare)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public IntegerLessThanPropertyValidator(Func<long> valueToCompareFunc) : base(() => (int)valueToCompareFunc.Invoke())
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public IntegerLessThanPropertyValidator(Func<T, long> valueToCompareFunc) : base(t => (int)valueToCompareFunc.Invoke(t))
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.LessThan;

    public override bool IsValid(int value, int valueToCompare)
    {
        return IntegerLessThanValidator.IsValid(value, valueToCompare);
    }
}