using FluentValidation;
using FluentValidation.Results;

namespace Lexicom.Validation;
public interface IValueValidator<TProperty> : IValidator<ValidationValue<TProperty>>
{
    ValidationResult Validate(TProperty instance);
    Task<ValidationResult> ValidateAsync(TProperty instance, CancellationToken cancellation = new());
}
