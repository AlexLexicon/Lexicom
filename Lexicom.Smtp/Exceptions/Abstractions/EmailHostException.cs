namespace Lexicom.Smtp.Exceptions.Abstractions;
public abstract class EmailHostException(string? message, Exception? innerException) : Exception(message, innerException)
{
}
