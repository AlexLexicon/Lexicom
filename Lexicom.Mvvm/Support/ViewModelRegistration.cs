using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Support;
public class ViewModelRegistration
{
    public ViewModelRegistration(
        ServiceLifetime serviceLifetime, 
        Type serviceType, 
        Type implementationType)
    {
        ServiceLifetime = serviceLifetime;
        ServiceType = serviceType;
        ImplementationType = implementationType;
    }

    public ServiceLifetime ServiceLifetime { get; }
    public Type ServiceType { get; }
    public Type ImplementationType { get; }
}
