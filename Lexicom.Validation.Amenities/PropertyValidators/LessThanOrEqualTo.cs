using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
public class LessThanOrEqualTo<T> : AbstractComparisonPropertyValidator<T, string?>
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
{
    public const string NAME = nameof(LessThanOrEqualTo<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be less than or equal to {ComparisonValue}.";

    public LessThanOrEqualTo(long valueToCompare) : base(valueToCompare.ToString())
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public LessThanOrEqualTo(Func<T, long> valueToCompareFunc) : base(t => valueToCompareFunc.Invoke(t).ToString())
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.LessThanOrEqual;

    public override bool IsValid(string? value, string? valueToCompare)
    {
        if (value is not null && long.TryParse(valueToCompare, out long longValueToCompare))
        {
            return value.Length <= longValueToCompare;
        }

        return true;
    }
}
