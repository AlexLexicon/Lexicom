using Lexicom.Smtp.Exceptions.Abstractions;

namespace Lexicom.Smtp.Exceptions;
public class EmailHostNotSpecifiedException(Exception? innerException) : EmailHostException("The email host was not specified.", innerException)
{
}