using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authorization;
public interface IAuthorizationServiceBuilder
{
    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }
}
public class AuthorizationServiceBuilder : IAuthorizationServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AuthorizationServiceBuilder(
        IServiceCollection services, 
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        Services = services;
        Configuration = configuration;
    }

    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }
}
