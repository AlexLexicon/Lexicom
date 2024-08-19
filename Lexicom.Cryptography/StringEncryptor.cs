using Lexicom.Cryptography.Exceptions;
using Lexicom.Cryptography.Extensions;
using System.Security.Cryptography;

namespace Lexicom.Cryptography;
public static class StringEncryptor
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SecretKeyEmptyException"/>
    /// <exception cref="SecretKeySizeException"/>
    public static string? Encrypt(IAesProvider aesProvider, byte[] secretKey, string? plainText)
    {
        ArgumentNullException.ThrowIfNull(aesProvider);
        ArgumentNullException.ThrowIfNull(secretKey);

        if (secretKey.Length is 0)
        {
            throw new SecretKeyEmptyException();
        }

        if (plainText is null)
        {
            return null;
        }

        using var aes = aesProvider.Create();

        SecretBitSize size = aes.CalculateSecretSize(secretKey);
        if (!size.IsValid)
        {
            throw new SecretKeySizeException(size);
        }

        using ICryptoTransform encryptor = aes.CreateEncryptor(secretKey, aes.IV);

        using var memoryStream = new MemoryStream();

        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))

        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(plainText);
        }

        byte[] iv = aes.IV;
        byte[] encryptedBytes = memoryStream.ToArray();

        byte[] ivAndEncryptedBytesComposite = new byte[iv.Length + encryptedBytes.Length];

        Buffer.BlockCopy(iv, 0, ivAndEncryptedBytesComposite, 0, iv.Length);
        Buffer.BlockCopy(encryptedBytes, 0, ivAndEncryptedBytesComposite, iv.Length, encryptedBytes.Length);

        return Convert.ToBase64String(ivAndEncryptedBytesComposite);
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SecretKeyEmptyException"/>
    /// <exception cref="SecretKeySizeException"/>
    public static async Task<string?> EncryptAsync(IAesProvider aesProvider, byte[] secretKey, string? plainText)
    {
        ArgumentNullException.ThrowIfNull(aesProvider);
        ArgumentNullException.ThrowIfNull(secretKey);

        if (secretKey.Length is 0)
        {
            throw new SecretKeyEmptyException();
        }

        if (plainText is null)
        {
            return null;
        }

        using var aes = aesProvider.Create();

        SecretBitSize size = aes.CalculateSecretSize(secretKey);
        if (!size.IsValid)
        {
            throw new SecretKeySizeException(size);
        }

        using ICryptoTransform encryptor = aes.CreateEncryptor(secretKey, aes.IV);

        using var memoryStream = new MemoryStream();

        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))

        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            await streamWriter.WriteAsync(plainText);
        }

        byte[] iv = aes.IV;
        byte[] encryptedBytes = memoryStream.ToArray();

        byte[] ivAndEncryptedBytesComposite = new byte[iv.Length + encryptedBytes.Length];

        Buffer.BlockCopy(iv, 0, ivAndEncryptedBytesComposite, 0, iv.Length);
        Buffer.BlockCopy(encryptedBytes, 0, ivAndEncryptedBytesComposite, iv.Length, encryptedBytes.Length);

        return Convert.ToBase64String(ivAndEncryptedBytesComposite);
    }
}