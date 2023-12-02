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
    /// <exception cref="NotValidatedOnStartupException{T}"/>
    public static void ThrowIfNull<TOption>([NotNull] TOption? optionsValue, [CallerArgumentExpression("optionsValue")] string optionsValueExpression = "")
    {
        if (optionsValue is null)
        {
            optionsValueExpression = optionsValueExpression.SimplifyCallerArgumentExpression();

            throw new NotValidatedOnStartupException<T>($"The options '{typeof(TOption).Name}' for '{optionsValueExpression}' was 'null' which is not valid but was configured to use a {nameof(AbstractOptionsValidator<T>)} at the application startup.").ToUnreachableException();
        }
    }

    public static UnreachableException ToUnreachableException()
    {
        return new NotValidatedOnStartupException<T>().ToUnreachableException();
    }
}
