using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Amenities;
public class MediatRHandlerImplementationConflictingWithViewModels<THandler>(THandler implementation, ServiceLifetime serviceLifetime)
{
    public ServiceLifetime ServiceLifetime { get; } = serviceLifetime;
    public THandler Implementation { get; } = implementation;
}
