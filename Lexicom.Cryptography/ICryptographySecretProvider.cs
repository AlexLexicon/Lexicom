namespace Lexicom.Cryptography;

public interface ICryptographySecretProvider
{
    byte[] GetSecret();
    Task<byte[]> GetSecretAsync();
}
