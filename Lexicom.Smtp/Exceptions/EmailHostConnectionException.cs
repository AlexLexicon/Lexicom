using Lexicom.Smtp.Exceptions.Abstractions;

namespace Lexicom.Smtp.Exceptions;
public class EmailHostConnectionException : EmailHostException
{
    public EmailHostConnectionException(Exception? innerException) : base("There was a problem connecting to the email host.", innerException)
    {
    }
}