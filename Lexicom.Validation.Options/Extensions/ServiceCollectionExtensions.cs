using Lexicom.Validation.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.Options.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddOptionsValidators<TAssemblyScanMarker>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddLexicomValidationValidators<TAssemblyScanMarker>(serviceLifetime);

        return services;
    }
}
