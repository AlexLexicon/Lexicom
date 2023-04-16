using FluentValidation;
using Lexicom.Extensions.Exceptions;
using Lexicom.Validation.Options.Exceptions;
using System.Diagnostics;

namespace Lexicom.Validation.Options;
public abstract class AbstractOptionsValidator<T> : AbstractValidator<T>
{
    public static UnreachableException ToUnreachableException()
    {
        return new NotValidatedOnStartupException<T>().ToUnreachableException();
    }
}
