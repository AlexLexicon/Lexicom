using Lexicom.Cryptography.Options;
using Lexicom.Cryptography.Validators;
using Microsoft.Extensions.Options;

namespace Lexicom.Cryptography;
public class CryptographyStringSecretProvider : ICryptographySecretProvider
{
    public static byte[] ConvertBase64SecretToBytes(string base64SecretKey)
    {
        return Convert.FromBase64String(base64SecretKey);
    }

    private readonly IOptions<CryptographyStringSecretOptions> _cryptographyStringSecretOptions;

    /// <exception cref="ArgumentNullException"/>
    public CryptographyStringSecretProvider(IOptions<CryptographyStringSecretOptions> cryptographyStringSecretOptions)
    {
        ArgumentNullException.ThrowIfNull(cryptographyStringSecretOptions);

        _cryptographyStringSecretOptions = cryptographyStringSecretOptions;
    }

    public Task<byte[]> GetSecretAsync()
    {
        CryptographyStringSecretOptions cryptographyStringSecretOptions = _cryptographyStringSecretOptions.Value;
        CryptographyStringSecretOptionsValidator.ThrowIfNull(cryptographyStringSecretOptions.Base64StringSecretKey);

        byte[] secretKey = ConvertBase64SecretToBytes(cryptographyStringSecretOptions.Base64StringSecretKey);

        return Task.FromResult(secretKey);
    }
}
