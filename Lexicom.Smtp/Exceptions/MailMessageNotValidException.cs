namespace Lexicom.Smtp.Exceptions;
public class MailMessageNotValidException(Exception? innerException) : Exception("The mail message was not valid", innerException)
{
}
