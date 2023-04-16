namespace Lexicom.Smtp.Exceptions;
public class MailMessageNotValidException : Exception
{
    public MailMessageNotValidException(Exception? innerException) : base("The mail message was not valid", innerException)
    {
    }
}
