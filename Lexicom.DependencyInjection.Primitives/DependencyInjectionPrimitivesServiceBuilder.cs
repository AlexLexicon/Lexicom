using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Primitives;
public interface IDependencyInjectionPrimitivesServiceBuilder
{
    IServiceCollection Services { get; }
}
public class DependencyInjectionPrimitivesServiceBuilder : IDependencyInjectionPrimitivesServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public DependencyInjectionPrimitivesServiceBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
    }

    public IServiceCollection Services { get; }
}
