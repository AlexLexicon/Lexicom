using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm;
public interface IMvvmServiceBuilder
{
    IServiceCollection Services { get; }
}
public class MvvmServiceBuilder : IMvvmServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public MvvmServiceBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
    }

    public IServiceCollection Services { get; }
}
