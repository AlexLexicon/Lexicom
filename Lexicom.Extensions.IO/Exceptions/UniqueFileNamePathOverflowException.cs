namespace Lexicom.Extensions.IO.Exceptions;
public class UniqueFileNamePathOverflowException : Exception
{
    public UniqueFileNamePathOverflowException(string? fileNamePath, int overflow) : base($"Failed to create a unique file name path from '{fileNamePath ?? "null"}'. Overflow after '{overflow}' attempts.")
    {
    }
}
