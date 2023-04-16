using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authentication.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAuthentication(this IServiceCollection services, IConfiguration configuration, Action<IAuthenticationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        configure?.Invoke(new AuthenticationServiceBuilder(services, configuration));

        return services;
    }
}
