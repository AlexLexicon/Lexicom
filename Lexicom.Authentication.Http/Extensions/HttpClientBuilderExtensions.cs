using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authentication.Http.Extensions;
public static class HttpClientBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IHttpClientBuilder AddHttpAuthenticationHandlers(this IHttpClientBuilder builder, Action<AuthenticationHttpClientBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var authenticationHttpClientBuilder = new AuthenticationHttpClientBuilder(builder);

        configure?.Invoke(authenticationHttpClientBuilder);

        authenticationHttpClientBuilder.Build();

        return builder;
    }
}
