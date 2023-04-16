namespace Lexicom.Smtp.Exceptions.Abstractions;
public abstract class EmailHostException : Exception
{
    protected EmailHostException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
