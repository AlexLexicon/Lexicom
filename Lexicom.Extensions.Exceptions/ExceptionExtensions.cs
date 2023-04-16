using System.Diagnostics;

namespace Lexicom.Extensions.Exceptions;
public static class ExceptionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static UnreachableException ToUnreachableException(this Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        return ToUnreachableException(exception, exception.Message ?? "");
    }
    /// <exception cref="ArgumentNullException"/>
    public static UnreachableException ToUnreachableException(this Exception exception, string message)
    {
        ArgumentNullException.ThrowIfNull(exception);
        ArgumentNullException.ThrowIfNull(message);

        return new UnreachableException($"[Unreachable] {message}", exception);
    }
}
