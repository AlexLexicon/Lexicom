using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Cryptography;
public interface ICryptographyServiceBuilder
{
    IServiceCollection Services { get; }
}
public class CryptographyServiceBuilder : ICryptographyServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public CryptographyServiceBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
    }

    public IServiceCollection Services { get; }
}
