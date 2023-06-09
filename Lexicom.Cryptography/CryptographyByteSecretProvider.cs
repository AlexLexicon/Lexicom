﻿using Lexicom.Cryptography.Options;
using Lexicom.Cryptography.Validators;
using Microsoft.Extensions.Options;

namespace Lexicom.Cryptography;
public class CryptographyByteSecretProvider : ICryptographySecretProvider
{
    private readonly IOptions<CryptographyByteSecretOptions> _cryptographyByteSecretOptions;

    /// <exception cref="ArgumentNullException"/>
    public CryptographyByteSecretProvider(IOptions<CryptographyByteSecretOptions> cryptographyByteSecretOptions)
    {
        ArgumentNullException.ThrowIfNull(cryptographyByteSecretOptions);

        _cryptographyByteSecretOptions = cryptographyByteSecretOptions;
    }

    public Task<byte[]> GetSecretAsync()
    {
        CryptographyByteSecretOptions cryptographyByteSecretOptions = _cryptographyByteSecretOptions.Value;
        CryptographyByteSecretOptionsValidator.ThrowIfNull(cryptographyByteSecretOptions.ByteArraySecretKey);

        return Task.FromResult(cryptographyByteSecretOptions.ByteArraySecretKey);
    }
}
