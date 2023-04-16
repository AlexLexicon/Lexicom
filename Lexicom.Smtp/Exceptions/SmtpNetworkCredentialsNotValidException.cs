namespace Lexicom.Smtp.Exceptions;
public class SmtpNetworkCredentialsNotValidException : Exception
{
    public SmtpNetworkCredentialsNotValidException() : base($"The smtp network credentials were not valid.")
    {
    }
}
