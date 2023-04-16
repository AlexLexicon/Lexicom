using Lexicom.Cryptography.Exceptions;
using System.Security.Cryptography;

namespace Lexicom.Cryptography;
public static class StringEncryptor
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SecretKeyNotValidException"/>
    public static string? Encrypt(byte[] secretKey, string? plainText)
    {
        ArgumentNullException.ThrowIfNull(secretKey);

        if (!secretKey.Any())
        {
            throw new SecretKeyNotValidException();
        }

        if (plainText is null)
        {
            return null;
        }

        using var aes = Aes.Create();

        using ICryptoTransform encryptor = aes.CreateEncryptor(secretKey, aes.IV);

        using var memoryStream = new MemoryStream();

        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))

        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(plainText);
        }

        byte[] iv = aes.IV;
        byte[] encryptedBytes = memoryStream.ToArray();

        var ivAndEncryptedBytesComposite = new byte[iv.Length + encryptedBytes.Length];

        Buffer.BlockCopy(iv, 0, ivAndEncryptedBytesComposite, 0, iv.Length);
        Buffer.BlockCopy(encryptedBytes, 0, ivAndEncryptedBytesComposite, iv.Length, encryptedBytes.Length);

        return Convert.ToBase64String(ivAndEncryptedBytesComposite);
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SecretKeyNotValidException"/>
    public static async Task<string?> EncryptAsync(byte[] secretKey, string? plainText)
    {
        ArgumentNullException.ThrowIfNull(secretKey);

        if (!secretKey.Any())
        {
            throw new SecretKeyNotValidException();
        }

        if (plainText is null)
        {
            return null;
        }

        using var aes = Aes.Create();

        using ICryptoTransform encryptor = aes.CreateEncryptor(secretKey, aes.IV);

        using var memoryStream = new MemoryStream();

        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))

        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            await streamWriter.WriteAsync(plainText);
        }

        byte[] iv = aes.IV;
        byte[] encryptedBytes = memoryStream.ToArray();

        var ivAndEncryptedBytesComposite = new byte[iv.Length + encryptedBytes.Length];

        Buffer.BlockCopy(iv, 0, ivAndEncryptedBytesComposite, 0, iv.Length);
        Buffer.BlockCopy(encryptedBytes, 0, ivAndEncryptedBytesComposite, iv.Length, encryptedBytes.Length);

        return Convert.ToBase64String(ivAndEncryptedBytesComposite);
    }
}