using FluentValidation.Validators;
using Lexicom.Validation.Amenities.Extensions;

namespace Lexicom.Validation.Amenities;

public abstract class AbstractPropertyValidator<T, TProperty> : PropertyValidator<T, TProperty>, IDefaultMessagePropertyValidator
{
    public abstract string DefaultMessageTemplate { get; }

    protected override string GetDefaultMessageTemplate(string? errorCode)
    {
        return DefaultMessagePropertyValidatorExtensions.GetLocalizedOrDefaultMessageTemplate(this, errorCode);
    }
}