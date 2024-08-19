using Lexicom.Cryptography.Exceptions;
using Lexicom.Cryptography.Extensions;
using System.Security.Cryptography;

namespace Lexicom.Cryptography;
public static class StringDecryptor
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SecretKeyEmptyException"/>
    /// <exception cref="SecretKeySizeException"/>
    public static string? Decrypt(IAesProvider aesProvider, string? encryptedBase64, byte[] secretKey)
    {
        ArgumentNullException.ThrowIfNull(aesProvider);
        ArgumentNullException.ThrowIfNull(secretKey);

        if (secretKey.Length is 0)
        {
            throw new SecretKeyEmptyException();
        }

        if (encryptedBase64 is null)
        {
            return null;
        }

        byte[] ivAndEncryptedBytesComposite = Convert.FromBase64String(encryptedBase64);

        byte[] iv = new byte[16];
        byte[] encryptedBytes = new byte[ivAndEncryptedBytesComposite.Length - iv.Length];

        Buffer.BlockCopy(ivAndEncryptedBytesComposite, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(ivAndEncryptedBytesComposite, iv.Length, encryptedBytes, 0, ivAndEncryptedBytesComposite.Length - iv.Length);

        using var aes = aesProvider.Create();

        SecretBitSize size = aes.CalculateSecretSize(secretKey);
        if (!size.IsValid)
        {
            throw new SecretKeySizeException(size);
        }

        using ICryptoTransform decryptor = aes.CreateDecryptor(secretKey, iv);

        string plainText;

        using (var memoryStream = new MemoryStream(encryptedBytes))
        {
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            using var streamWriter = new StreamReader(cryptoStream);

            plainText = streamWriter.ReadToEnd();
        }

        return plainText;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SecretKeyEmptyException"/>
    /// <exception cref="SecretKeySizeException"/>
    public static async Task<string?> DecryptAsync(IAesProvider aesProvider, string? encryptedBase64, byte[] secretKey)
    {
        ArgumentNullException.ThrowIfNull(aesProvider);
        ArgumentNullException.ThrowIfNull(secretKey);

        if (secretKey.Length is 0)
        {
            throw new SecretKeyEmptyException();
        }

        if (encryptedBase64 is null)
        {
            return null;
        }

        byte[] ivAndEncryptedBytesComposite = Convert.FromBase64String(encryptedBase64);

        byte[] iv = new byte[16];
        byte[] encryptedBytes = new byte[ivAndEncryptedBytesComposite.Length - iv.Length];

        Buffer.BlockCopy(ivAndEncryptedBytesComposite, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(ivAndEncryptedBytesComposite, iv.Length, encryptedBytes, 0, ivAndEncryptedBytesComposite.Length - iv.Length);

        using var aes = aesProvider.Create();

        SecretBitSize size = aes.CalculateSecretSize(secretKey);
        if (!size.IsValid)
        {
            throw new SecretKeySizeException(size);
        }

        using ICryptoTransform decryptor = aes.CreateDecryptor(secretKey, iv);

        string plainText;

        using (var memoryStream = new MemoryStream(encryptedBytes))
        {
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            using var streamWriter = new StreamReader(cryptoStream);

            plainText = await streamWriter.ReadToEndAsync();
        }

        return plainText;
    }
}
