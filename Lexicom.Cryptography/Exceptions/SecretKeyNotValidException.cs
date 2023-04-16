namespace Lexicom.Cryptography.Exceptions;
public class SecretKeyNotValidException : Exception
{
    public SecretKeyNotValidException() : base("The secret key is not valid")
    {
    }
}
