using Lexicom.Smtp.Exceptions.Abstractions;

namespace Lexicom.Smtp.Exceptions;
public class EmailHostUnKnownException : EmailHostException
{
    public EmailHostUnKnownException(Exception? innerException) : base("The email host is unknown.", innerException)
    {
    }
}
