using Lexicom.Smtp.Exceptions.Abstractions;

namespace Lexicom.Smtp.Exceptions;

public class EmailHostUnknownException(Exception? innerException) : EmailHostException("The email host is unknown.", innerException)
{
}
