using FluentValidation.Resources;
using FluentValidation;

namespace Lexicom.Validation.Exceptions;
public class LanguageManagerNotDerivedFromLanguageManagerException : Exception
{
    public LanguageManagerNotDerivedFromLanguageManagerException() : base($"The Fluent Validation '{nameof(ValidatorOptions.Global)}.{nameof(ValidatorOptions.Global.LanguageManager)}' was not derived from the type '{typeof(LanguageManager).Name}'")
    {
    }
}
