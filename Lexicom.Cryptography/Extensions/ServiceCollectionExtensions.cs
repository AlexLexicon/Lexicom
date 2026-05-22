using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Cryptography.Extensions;

public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomCryptography(this IServiceCollection services, Action<ICryptographyServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ICryptographyService, CryptographyService>();
        services.AddSingleton<IAesProvider, AesProvider>();
        services.AddSingleton<ICiphertextAuthenticator, CiphertextAuthenticator>();

        configure?.Invoke(new CryptographyServiceBuilder(services));

        return services;
    }
}