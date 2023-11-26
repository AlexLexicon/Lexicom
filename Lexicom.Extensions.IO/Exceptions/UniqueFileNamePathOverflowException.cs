namespace Lexicom.Extensions.IO.Exceptions;
public class UniqueFileNamePathOverflowException(string? fileNamePath, int overflow) : Exception($"Failed to create a unique file name path from '{fileNamePath ?? "null"}'. Overflow after '{overflow}' attempts.")
{
}
