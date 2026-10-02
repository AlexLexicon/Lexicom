using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;

public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAspNetCoreControllersAuthenticationAccessTokenAuthentication(this IServiceCollection services, Action<IAuthenticationAccessTokenBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var authenticationBearerTokenBuilder = new AspNetCoreAuthenticationAccessTokenBuilder(services);

        configure?.Invoke(authenticationBearerTokenBuilder);

        authenticationBearerTokenBuilder.Build();

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAspNetCoreControllersAuthenticationApiKeyAuthentication(this IServiceCollection services, Action<IAuthenticationApiKeyBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var authenticationApiKeyBuilder = new AspNetCoreAuthenticationApiKeyBuilder(services);

        configure?.Invoke(authenticationApiKeyBuilder);

        authenticationApiKeyBuilder.Build();

        return services;
    }
}
