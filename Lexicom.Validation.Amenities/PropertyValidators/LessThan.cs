using FluentValidation.Validators;

namespace Lexicom.Validation.Amenities.PropertyValidators;
#pragma warning disable CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
public class LessThan<T> : AbstractComparisonPropertyValidator<T, string?>
#pragma warning restore CS8631 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match constraint type.
{
    public const string NAME = nameof(LessThan<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be less than {ComparisonValue}.";

    public LessThan(long value) : base(value.ToString())
    {
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;
    public override Comparison Comparison => Comparison.LessThan;

    public override bool IsValid(string? value, string? valueToCompare)
    {
        if (long.TryParse(value, out long longValue) && long.TryParse(valueToCompare, out long longValueToCompare))
        {
            return longValue < longValueToCompare;
        }

        return true;
    }
}