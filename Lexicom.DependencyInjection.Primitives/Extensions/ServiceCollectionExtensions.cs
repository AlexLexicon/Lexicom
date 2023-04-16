using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Primitives.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomTimeProvider(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ITimeProvider>(TimeProvider.System);

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomGuidProvider(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IGuidProvider, GuidProvider>();

        return services;
    }
}