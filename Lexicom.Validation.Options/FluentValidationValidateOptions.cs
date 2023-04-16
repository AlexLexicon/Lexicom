using FluentValidation;
using FluentValidation.Results;
using Lexicom.Validation.Extensions;
using Microsoft.Extensions.Options;

namespace Lexicom.Validation.Options;
public class FluentValidationValidateOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
    private readonly string _name;
    private readonly IValidator<TOptions> _validator;

    /// <exception cref="ArgumentNullException"/>
    public FluentValidationValidateOptions(
        string name,
        IValidator<TOptions> validator)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(validator);

        _name = name;
        _validator = validator;
    }

    /// <exception cref="ArgumentNullException"/>
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (_name is not null && _name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        ValidationResult validationResult = _validator.Validate(options);

        if (!validationResult.IsValid)
        {
            var errorMessages = new List<string>();
            foreach (ValidationFailure? validationFailure in validationResult.Errors.StandardizeErrorMessages())
            {
                errorMessages.Add($"'{(string.IsNullOrWhiteSpace(name) ? typeof(TOptions).Name : name)}.{validationFailure.PropertyName}': {validationFailure.ErrorMessage.TrimEnd('.')}");
            }

            return ValidateOptionsResult.Fail(errorMessages);
        }

        return ValidateOptionsResult.Success;
    }
}
