using Lexicom.Cryptography.Options;
using Lexicom.Cryptography.Validators;
using Microsoft.Extensions.Options;

namespace Lexicom.Cryptography;
public class CryptographyStringSecretProvider : ICryptographySecretProvider
{
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

        byte[] secretKey = Convert.FromBase64String(cryptographyStringSecretOptions.Base64StringSecretKey);

        return Task.FromResult(secretKey);
    }
}
