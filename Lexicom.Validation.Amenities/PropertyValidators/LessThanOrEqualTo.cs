using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public class LessThanOrEqualTo<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(DigitsPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be less than or equal to {ComparisonValue}.";

    private readonly long _minimum;

    public LessThanOrEqualTo(long minimum)
    {
        _minimum = minimum;
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (long.TryParse(value, out long longValue))
        {
            return longValue <= _minimum;
        }

        return true;
    }
}