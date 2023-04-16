using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authorization.Http.Extensions;
public static class HttpClientBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IHttpClientBuilder AddLexicomAuthenticationHandler(this IHttpClientBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.AddHttpMessageHandler<HttpClientAuthenticationHandler>();

        return builder;
    }
}
