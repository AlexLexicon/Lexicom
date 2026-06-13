using Lexicom.Cryptography.Exceptions;
using Lexicom.Cryptography.Extensions;
using System.Security.Cryptography;

namespace Lexicom.Cryptography;

public static class StringDecryptor
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SecretKeyEmptyException"/>
    /// <exception cref="SecretKeySizeException"/>
    /// <exception cref="EncryptedTextNotValidException"/>
    public static string? Decrypt(IAesProvider aesProvider, ICiphertextAuthenticator ciphertextAuthenticator, string? encryptedBase64, byte[] secretKey)
    {
        ArgumentNullException.ThrowIfNull(aesProvider);
        ArgumentNullException.ThrowIfNull(ciphertextAuthenticator);
        ArgumentNullException.ThrowIfNull(secretKey);

        if (secretKey.Length is 0)
        {
            throw new SecretKeyEmptyException();
        }

        if (encryptedBase64 is null)
        {
            return null;
        }

        byte[] composite = Convert.FromBase64String(encryptedBase64);

        (int initializationVectorByteCount, int authenticationTagByteCount) = ciphertextAuthenticator.GetByteCountsAndValidateComposite(composite);

        int ivAndEncryptedBytesLength = composite.Length - authenticationTagByteCount;

        byte[] ivAndEncryptedBytes = new byte[ivAndEncryptedBytesLength];
        byte[] authenticationTag = new byte[authenticationTagByteCount];

        Buffer.BlockCopy(composite, 0, ivAndEncryptedBytes, 0, ivAndEncryptedBytesLength);
        Buffer.BlockCopy(composite, ivAndEncryptedBytesLength, authenticationTag, 0, authenticationTag.Length);

        using var aes = aesProvider.Create();

        SecretBitSize size = aes.CalculateSecretSize(secretKey);
        if (!size.IsValid)
        {
            throw new SecretKeySizeException(size);
        }

        //verify the authentication tag before decrypting (encrypt-then-MAC) so tampered or non authentic ciphertext is rejected
        byte[] expectedAuthenticationTag = ciphertextAuthenticator.ComputeAuthenticationTag(secretKey, ivAndEncryptedBytes);

        if (!CryptographicOperations.FixedTimeEquals(expectedAuthenticationTag, authenticationTag))
        {
            throw new EncryptedTextNotValidException();
        }

        byte[] iv = new byte[initializationVectorByteCount];
        byte[] encryptedBytes = new byte[ivAndEncryptedBytesLength - iv.Length];

        Buffer.BlockCopy(ivAndEncryptedBytes, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(ivAndEncryptedBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

        using ICryptoTransform decryptor = aes.CreateDecryptor(secretKey, iv);

        string plainText;

        using (var memoryStream = new MemoryStream(encryptedBytes))
        {
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            using var streamReader = new StreamReader(cryptoStream);

            plainText = streamReader.ReadToEnd();
        }

        return plainText;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="SecretKeyEmptyException"/>
    /// <exception cref="SecretKeySizeException"/>
    /// <exception cref="EncryptedTextNotValidException"/>
    public static async Task<string?> DecryptAsync(IAesProvider aesProvider, ICiphertextAuthenticator ciphertextAuthenticator, string? encryptedBase64, byte[] secretKey)
    {
        ArgumentNullException.ThrowIfNull(aesProvider);
        ArgumentNullException.ThrowIfNull(ciphertextAuthenticator);
        ArgumentNullException.ThrowIfNull(secretKey);

        if (secretKey.Length is 0)
        {
            throw new SecretKeyEmptyException();
        }

        if (encryptedBase64 is null)
        {
            return null;
        }

        byte[] composite = Convert.FromBase64String(encryptedBase64);

        (int initializationVectorByteCount, int authenticationTagByteCount) = ciphertextAuthenticator.GetByteCountsAndValidateComposite(composite);

        int ivAndEncryptedBytesLength = composite.Length - authenticationTagByteCount;

        byte[] ivAndEncryptedBytes = new byte[ivAndEncryptedBytesLength];
        byte[] authenticationTag = new byte[authenticationTagByteCount];

        Buffer.BlockCopy(composite, 0, ivAndEncryptedBytes, 0, ivAndEncryptedBytesLength);
        Buffer.BlockCopy(composite, ivAndEncryptedBytesLength, authenticationTag, 0, authenticationTag.Length);

        using var aes = aesProvider.Create();

        SecretBitSize size = aes.CalculateSecretSize(secretKey);
        if (!size.IsValid)
        {
            throw new SecretKeySizeException(size);
        }

        //verify the authentication tag before decrypting (encrypt-then-MAC) so tampered or non authentic ciphertext is rejected
        byte[] expectedAuthenticationTag = ciphertextAuthenticator.ComputeAuthenticationTag(secretKey, ivAndEncryptedBytes);

        if (!CryptographicOperations.FixedTimeEquals(expectedAuthenticationTag, authenticationTag))
        {
            throw new EncryptedTextNotValidException();
        }

        byte[] iv = new byte[initializationVectorByteCount];
        byte[] encryptedBytes = new byte[ivAndEncryptedBytesLength - iv.Length];

        Buffer.BlockCopy(ivAndEncryptedBytes, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(ivAndEncryptedBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

        using ICryptoTransform decryptor = aes.CreateDecryptor(secretKey, iv);

        string plainText;

        await using (var memoryStream = new MemoryStream(encryptedBytes))
        {
            await using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            using var streamReader = new StreamReader(cryptoStream);

            plainText = await streamReader.ReadToEndAsync();
        }

        return plainText;
    }
}
