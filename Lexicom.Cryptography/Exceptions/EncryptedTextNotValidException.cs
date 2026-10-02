namespace Lexicom.Cryptography.Exceptions;

public class EncryptedTextNotValidException() : Exception("The encrypted text could not be authenticated. It may have been tampered with or the wrong secret key was used.")
{
}
