using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Lexicom.EntityFramework.Exceptions;
public class NonNullableTableColumnException : Exception
{
    public static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument is null)
        {
            throw new NonNullableTableColumnException(paramName);
        }
    }

#pragma warning disable IDE0060 // Remove unused parameter
    public NonNullableTableColumnException(object? argument, [CallerArgumentExpression(nameof(argument))] string? argumentText = null) : this(argumentText)
    {
    }
#pragma warning restore IDE0060 // Remove unused parameter
    public NonNullableTableColumnException(string? identifier) : base($"The {(identifier is null ? string.Empty : $"{identifier} ")}column was null but this table column should be non nullable.")
    {
    }
}
