using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authentication.For.ConsoleApp.Extensions;

public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomConsoleAppAuthenticationAccessTokenAuthentication(this IServiceCollection services, Action<IAuthenticationAccessTokenBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var authenticationBearerTokenBuilder = new AuthenticationAccessTokenBuilder(services);

        configure?.Invoke(authenticationBearerTokenBuilder);

        authenticationBearerTokenBuilder.Build();

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomConsoleAppAuthenticationApiKeyAuthentication(this IServiceCollection services, Action<IAuthenticationApiKeyBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var authenticationApiKeyBuilder = new AuthenticationApiKeyBuilder(services);

        configure?.Invoke(authenticationApiKeyBuilder);

        authenticationApiKeyBuilder.Build();

        return services;
    }
}
