using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAspNetCoreControllersAuthenticationAccessTokenAuthentication(this IServiceCollection services, Action<IAuthenticationAccessTokenBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var authenticationBearerTokenBuilder = new AuthenticationAccessTokenBuilder(services);

        configure?.Invoke(authenticationBearerTokenBuilder);

        authenticationBearerTokenBuilder.Build();

        return services;
    }
}
