using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm;
public interface IViewModelServiceBuilder
{
    IServiceCollection Services { get; }
    Type ServiceType { get; }
    Type ImplementationType { get; }
    ServiceLifetime ServiceLifetime { get; set; }
}
public class ViewModelServiceBuilder : IViewModelServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public ViewModelServiceBuilder(
        IServiceCollection services,
        Type serviceType,
        Type implementationType)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(implementationType);

        Services = services;
        ServiceType = serviceType;
        ImplementationType = implementationType;
    }

    public IServiceCollection Services { get; }
    public Type ServiceType { get; }
    public Type ImplementationType { get; }
    public ServiceLifetime ServiceLifetime { get; set; }
}
