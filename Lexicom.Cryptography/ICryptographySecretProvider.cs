namespace Lexicom.Cryptography;
public interface ICryptographySecretProvider
{
    Task<byte[]> GetSecretAsync();
}
