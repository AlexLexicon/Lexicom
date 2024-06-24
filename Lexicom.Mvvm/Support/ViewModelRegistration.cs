using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Support;
public class ViewModelRegistration
{
    public required ServiceLifetime ServiceLifetime { get; init; }
    public required Type ServiceType { get; init; }
    public required Type ImplementationType { get; init; }
    internal bool IsSingletonCreated { get; set; }
}
