using FluentValidation;
using FluentValidation.Results;

namespace Lexicom.Validation;
public abstract class AbstractValueValidator<TProperty> : AbstractValidator<ValidationValue<TProperty>>, IValueValidator<TProperty>
{
    public virtual ValidationResult Validate(TProperty instance)
    {
        return Validate(new ValidationValue<TProperty>
        {
            Value = instance
        });
    }

    public virtual Task<ValidationResult> ValidateAsync(TProperty instance, CancellationToken cancellation = default)
    {
        return ValidateAsync(new ValidationValue<TProperty>
        {
            Value = instance
        }, cancellation);
    }
}
