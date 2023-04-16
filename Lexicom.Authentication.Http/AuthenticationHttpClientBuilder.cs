using Lexicom.Authentication.Http.DelegatingHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Authentication.Http;
public class AuthenticationHttpClientBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AuthenticationHttpClientBuilder(IHttpClientBuilder httpClientBuilder)
    {
        Builder = httpClientBuilder;
    }

    public IHttpClientBuilder Builder { get; }

    private bool IncludeAccessTokenHttpClientDelegatingHandler { get; set; }
    private bool IncludeRefreshTokenHttpClientDelegatingHandler { get; set; }
    private bool IncludeUnauthorizedHttpClientDelegatingHandler { get; set; }

    public void AuthorizeWithAccessToken() => IncludeAccessTokenHttpClientDelegatingHandler = true;
    public void AutomaticallyRefreshAccessToken() => IncludeRefreshTokenHttpClientDelegatingHandler = true;
    public void ForwardUnathorizedRequests() => IncludeUnauthorizedHttpClientDelegatingHandler = true;

    public void Build()
    {
        //the order here matters
        //which is why we use
        //the include booleans

        if (IncludeUnauthorizedHttpClientDelegatingHandler)
        {
            Builder.Services.TryAddSingleton<UnauthorizedHttpClientDelegatingHandler>();

            Builder.AddHttpMessageHandler<UnauthorizedHttpClientDelegatingHandler>();
        }

        if (IncludeRefreshTokenHttpClientDelegatingHandler)
        {
            Builder.Services.TryAddSingleton<RefreshTokenHttpClientDelegatingHandler>();

            Builder.AddHttpMessageHandler<RefreshTokenHttpClientDelegatingHandler>();
        }

        if (IncludeAccessTokenHttpClientDelegatingHandler)
        {
            Builder.Services.TryAddSingleton<AccessTokenHttpClientDelegatingHandler>();

            Builder.AddHttpMessageHandler<AccessTokenHttpClientDelegatingHandler>();
        }
    }
}
