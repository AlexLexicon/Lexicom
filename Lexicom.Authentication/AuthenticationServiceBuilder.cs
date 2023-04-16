using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authentication;
public interface IAuthenticationServiceBuilder
{
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
}
public class AuthenticationServiceBuilder : IAuthenticationServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AuthenticationServiceBuilder(
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
