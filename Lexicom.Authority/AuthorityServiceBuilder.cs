using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authority;
public interface IAuthorityServiceBuilder
{
    IServiceCollection Services { get; }
}
public class AuthorityServiceBuilder : IAuthorityServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AuthorityServiceBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
    }

    public IServiceCollection Services { get; }
}
