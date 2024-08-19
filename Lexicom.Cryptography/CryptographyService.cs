using Lexicom.Cryptography.Options;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Lexicom.Cryptography;
public interface ICryptographyService
{
    //static creation of the ICryptographyService
    //useful for when you need to decrypt in a non DependencyInjection context
    //or before a service provider is available
    //like for example a connection string or some other configuration value
    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyService Create(byte[] byteArraySecretKey)
    {
        ArgumentNullException.ThrowIfNull(byteArraySecretKey);

        return Create(new CryptographyByteSecretOptions
        {
            ByteArraySecretKey = byteArraySecretKey
        });
    }
    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyService Create(byte[] byteArraySecretKey, IAesProvider aesProvider)
    {
        ArgumentNullException.ThrowIfNull(byteArraySecretKey);
        ArgumentNullException.ThrowIfNull(aesProvider);

        return Create(new CryptographyByteSecretOptions
        {
            ByteArraySecretKey = byteArraySecretKey
        }, aesProvider);
    }
    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyService Create(string base64StringSecretKey)
    {
        ArgumentNullException.ThrowIfNull(base64StringSecretKey);

        return Create(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = base64StringSecretKey
        });
    }
    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyService Create(string base64StringSecretKey, IAesProvider aesProvider)
    {
        ArgumentNullException.ThrowIfNull(base64StringSecretKey);
        ArgumentNullException.ThrowIfNull(aesProvider);

        return Create(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = base64StringSecretKey
        }, aesProvider);
    }
    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyService Create(CryptographyByteSecretOptions cryptographyByteSecretOptions)
    {
        ArgumentNullException.ThrowIfNull(cryptographyByteSecretOptions);

        return Create(cryptographyByteSecretOptions, new AesProvider());
    }
    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyService Create(CryptographyByteSecretOptions cryptographyByteSecretOptions, IAesProvider aesProvider)
    {
        ArgumentNullException.ThrowIfNull(cryptographyByteSecretOptions);
        ArgumentNullException.ThrowIfNull(aesProvider);

        IOptions<CryptographyByteSecretOptions> options = Microsoft.Extensions.Options.Options.Create(cryptographyByteSecretOptions);

        return new CryptographyService(new CryptographyByteSecretProvider(options), aesProvider);
    }
    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyService Create(CryptographyStringSecretOptions cryptographyStringSecretOptions)
    {
        ArgumentNullException.ThrowIfNull(cryptographyStringSecretOptions);

        return Create(cryptographyStringSecretOptions, new AesProvider());
    }
    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyService Create(CryptographyStringSecretOptions cryptographyStringSecretOptions, IAesProvider aesProvider)
    {
        ArgumentNullException.ThrowIfNull(cryptographyStringSecretOptions);
        ArgumentNullException.ThrowIfNull(aesProvider);

        IOptions<CryptographyStringSecretOptions> options = Microsoft.Extensions.Options.Options.Create(cryptographyStringSecretOptions);

        return new CryptographyService(new CryptographyStringSecretProvider(options), aesProvider);
    }

    /// <exception cref="ArgumentNullException"/>
    string Encrypt(string plainText);
    string? EncryptOrNull(string? plainText);
    /// <exception cref="ArgumentNullException"/>
    Task<string> EncryptAsync(string plainText);
    Task<string?> EncryptOrNullAsync(string? plainText);
    /// <exception cref="ArgumentNullException"/>
    string Decrypt(string encryptedBase64);
    string? DecryptOrNull(string? encryptedBase64);
    /// <exception cref="ArgumentNullException"/>
    Task<string> DecryptAsync(string encryptedBase64);
    Task<string?> DecryptOrNullAsync(string? encryptedBase64);
}
public class CryptographyService : ICryptographyService
{
    private readonly ICryptographySecretProvider _cryptographySecretProvider;
    private readonly IAesProvider _aesProvider;

    /// <exception cref="ArgumentNullException"/>
    public CryptographyService(
        ICryptographySecretProvider cryptographySecretProvider, 
        IAesProvider aesProvider)
    {
        ArgumentNullException.ThrowIfNull(cryptographySecretProvider);
        ArgumentNullException.ThrowIfNull(aesProvider);

        _cryptographySecretProvider = cryptographySecretProvider;
        _aesProvider = aesProvider;
    }

    /// <exception cref="ArgumentNullException"/>
    public string Encrypt(string plainText)
    {
        ArgumentNullException.ThrowIfNull(plainText);

        string? encryptedString = EncryptOrNull(plainText);

        if (encryptedString is null)
        {
            throw new UnreachableException($"'{nameof(EncryptOrNull)}' should only return null when the '{plainText}' parameter is null which is never true.");
        }

        return encryptedString;
    }

    public string? EncryptOrNull(string? plainText)
    {
        byte[] secretKey = _cryptographySecretProvider.GetSecretAsync().Result;

        return StringEncryptor.Encrypt(_aesProvider, secretKey, plainText);
    }

    /// <exception cref="ArgumentNullException"/>
    public async Task<string> EncryptAsync(string plainText)
    {
        ArgumentNullException.ThrowIfNull(plainText);

        string? encryptedString = await EncryptOrNullAsync(plainText);

        if (encryptedString is null)
        {
            throw new UnreachableException($"'{nameof(EncryptOrNullAsync)}' should only return null when the '{plainText}' parameter is null which is never true.");
        }

        return encryptedString;
    }

    public async Task<string?> EncryptOrNullAsync(string? plainText)
    {
        byte[] secretKey = await _cryptographySecretProvider.GetSecretAsync();

        return await StringEncryptor.EncryptAsync(_aesProvider, secretKey, plainText);
    }

    /// <exception cref="ArgumentNullException"/>
    public string Decrypt(string encryptedBase64)
    {
        ArgumentNullException.ThrowIfNull(encryptedBase64);

        string? decryptedString = DecryptOrNull(encryptedBase64);

        if (decryptedString is null)
        {
            throw new UnreachableException($"'{nameof(DecryptOrNull)}' should only return null when the '{encryptedBase64}' parameter is null which is never true.");
        }

        return decryptedString;
    }

    public string? DecryptOrNull(string? encryptedBase64)
    {
        byte[] secretKey = _cryptographySecretProvider.GetSecretAsync().Result;

        return StringDecryptor.Decrypt(_aesProvider, encryptedBase64, secretKey);
    }

    /// <exception cref="ArgumentNullException"/>
    public async Task<string> DecryptAsync(string encryptedBase64)
    {
        ArgumentNullException.ThrowIfNull(encryptedBase64);

        string? decryptedString = await DecryptOrNullAsync(encryptedBase64);

        if (decryptedString is null)
        {
            throw new UnreachableException($"'{nameof(DecryptOrNullAsync)}' should only return null when the '{encryptedBase64}' parameter is null which is never true.");
        }

        return decryptedString;
    }

    public async Task<string?> DecryptOrNullAsync(string? encryptedBase64)
    {
        byte[] secretKey = await _cryptographySecretProvider.GetSecretAsync();

        return await StringDecryptor.DecryptAsync(_aesProvider, encryptedBase64, secretKey);
    }
}