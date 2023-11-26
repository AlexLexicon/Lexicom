using Lexicom.Smtp.Exceptions.Abstractions;

namespace Lexicom.Smtp.Exceptions;
public class EmailHostConnectionException(Exception? innerException) : EmailHostException("There was a problem connecting to the email host.", innerException)
{
}