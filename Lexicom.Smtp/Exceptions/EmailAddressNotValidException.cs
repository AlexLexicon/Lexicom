namespace Lexicom.Smtp.Exceptions;
public class EmailAddressNotValidException(string? emailAddress, Exception? innerException) : Exception($"The email address '{emailAddress ?? "null"}' was not valid.", innerException)
{
    public EmailAddressNotValidException(string? emailAddress) : this(emailAddress, null)
    {
    }
}
