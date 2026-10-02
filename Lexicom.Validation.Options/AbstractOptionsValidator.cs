using FluentValidation;
using Lexicom.Extensions.CompilerServices;
using Lexicom.Extensions.Exceptions;
using Lexicom.Validation.Options.Exceptions;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Lexicom.Validation.Options;

public abstract class AbstractOptionsValidator<T> : AbstractValidator<T>
{
    /// <exception cref="UnreachableException"/>
    public static void ThrowIfNull<TOption>([NotNull] TOption? optionsValue, [CallerArgumentExpression(nameof(optionsValue))] string optionsValueExpression = "")
    {
        if (optionsValue is null)
        {
            optionsValueExpression = optionsValueExpression.SimplifyCallerArgumentExpression();

            throw ToUnreachableException($"The options '{typeof(TOption).Name}' for '{optionsValueExpression}' was 'null' which is not valid but was configured to use a {nameof(AbstractOptionsValidator<>)} at the application startup.");
        }
    }

    public static UnreachableException ToUnreachableException(string? message = null)
    {
        Exception exception;
        if (message is null)
        {
            exception = new NotValidatedOnStartupException<T>();
        }
        else
        {
            exception = new NotValidatedOnStartupException<T>(message);
        }

        return exception.ToUnreachableException();
    }
}
