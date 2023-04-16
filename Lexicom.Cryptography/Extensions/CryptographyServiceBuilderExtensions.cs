using Lexicom.Cryptography.Options;
using Lexicom.Cryptography.Validators;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Validation.Options.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Cryptography.Extensions;
public static class CryptographyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyServiceBuilder AddByteSecretOptions(this ICryptographyServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services
            .AddOptions<CryptographyByteSecretOptions>()
            .BindConfiguration()
            .Validate<CryptographyByteSecretOptions, CryptographyByteSecretOptionsValidator>()
            .ValidateOnStart();

        builder.Services.AddSingleton<ICryptographySecretProvider, CryptographyByteSecretProvider>();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static ICryptographyServiceBuilder AddStringSecretOptions(this ICryptographyServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services
            .AddOptions<CryptographyStringSecretOptions>()
            .BindConfiguration()
            .Validate<CryptographyStringSecretOptions, CryptographyStringSecretOptionsValidator>()
            .ValidateOnStart();

        builder.Services.AddSingleton<ICryptographySecretProvider, CryptographyStringSecretProvider>();

        return builder;
    }
}
