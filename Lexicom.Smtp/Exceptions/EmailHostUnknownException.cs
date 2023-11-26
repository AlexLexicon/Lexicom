using Lexicom.Smtp.Exceptions.Abstractions;

namespace Lexicom.Smtp.Exceptions;
public class EmailHostUnKnownException(Exception? innerException) : EmailHostException("The email host is unknown.", innerException)
{
}
