using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public class GreaterThanOrEqualTo<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(GreaterThanOrEqualTo<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be greater than or equal to {ComparisonValue}.";

    private readonly long _maximum;

    public GreaterThanOrEqualTo(long maximum)
    {
        _maximum = maximum;
    }

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (long.TryParse(value, out long longValue))
        {
            return longValue >= _maximum;
        }

        return true;
    }
}