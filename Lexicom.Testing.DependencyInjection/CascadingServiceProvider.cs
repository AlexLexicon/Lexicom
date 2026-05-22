using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Testing.DependencyInjection;

public class CascadingServiceProvider : IServiceProvider
{
    public CascadingServiceProvider(
        IServiceCollection services,
        IServiceProvider? childServiceProvider)
    {
        ArgumentNullException.ThrowIfNull(services);

        ServiceProvider = services.BuildServiceProvider();
        ChildServiceProvider = childServiceProvider;
    }

    private IServiceProvider? ChildServiceProvider { get; }
    private ServiceProvider ServiceProvider { get; }

    public object? GetService(Type serviceType)
    {
        object? service = ChildServiceProvider?.GetService(serviceType);
        if (service is not null)
        {
            return service;
        }

        return ServiceProvider.GetService(serviceType);
    }
}
