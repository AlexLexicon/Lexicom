using Lexicom.Smtp.Exceptions.Abstractions;

namespace Lexicom.Smtp.Exceptions;
public class EmailHostNotSpecifiedException : EmailHostException
{
    public EmailHostNotSpecifiedException(Exception? innerException) : base("The email host was not specified.", innerException)
    {
    }
}