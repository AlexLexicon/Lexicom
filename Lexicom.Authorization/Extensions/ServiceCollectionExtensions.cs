using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authorization.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAuthorization(this IServiceCollection services, IConfiguration configuration, Action<IAuthorizationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        configure?.Invoke(new AuthorizationServiceBuilder(services, configuration));

        return services;
    }
}
