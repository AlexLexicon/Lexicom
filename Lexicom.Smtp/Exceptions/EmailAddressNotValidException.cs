namespace Lexicom.Smtp.Exceptions;
public class EmailAddressNotValidException : Exception
{
    public EmailAddressNotValidException(string? emailAddress) : this(emailAddress, null)
    {
    }
    public EmailAddressNotValidException(string? emailAddress, Exception? innerException) : base($"The email address '{emailAddress ?? "null"}' was not valid.", innerException)
    {
    }
}
