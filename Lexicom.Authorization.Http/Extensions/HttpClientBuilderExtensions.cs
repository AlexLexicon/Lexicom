using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Authorization.Http.Extensions;

public static class HttpClientBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IHttpClientBuilder AddLexicomAuthenticationHandler(this IHttpClientBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.TryAddTransient<HttpClientAuthenticationHandler>();

        builder.AddHttpMessageHandler<HttpClientAuthenticationHandler>();

        return builder;
    }
}
