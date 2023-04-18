using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Primitives.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomDependencyInjectionPrimitives(this IServiceCollection services, Action<IDependencyInjectionPrimitivesServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        configure?.Invoke(new DependencyInjectionPrimitivesServiceBuilder(services));

        return services;
    }
}