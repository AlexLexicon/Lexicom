using Lexicom.DependencyInjection.Primitives.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Primitives;
public class RandomProviderFactory : IRandomProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RandomProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IRandomProvider Create()
    {
        return ActivatorUtilities.CreateInstance<IRandomProvider>(_serviceProvider);
    }

    public IRandomProvider Create(int seed)
    {
        return ActivatorUtilities.CreateInstance<IRandomProvider>(_serviceProvider, seed);
    }
}
