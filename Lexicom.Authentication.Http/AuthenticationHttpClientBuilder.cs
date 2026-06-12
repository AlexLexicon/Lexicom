using Lexicom.Authentication.Http.DelegatingHandlers;
using Lexicom.Authentication.Http.Exceptions;
using Lexicom.Authentication.Http.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Authentication.Http;

public class AuthenticationHttpClientBuilder
{
    public IHttpClientBuilder Builder { get; }

    private bool IncludeAccessTokenHttpClientDelegatingHandler { get; set; }
    private bool IncludeRefreshTokenHttpClientDelegatingHandler { get; set; }
    private bool IncludeUnauthorizedHttpClientDelegatingHandler { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public AuthenticationHttpClientBuilder(IHttpClientBuilder httpClientBuilder)
    {
        ArgumentNullException.ThrowIfNull(httpClientBuilder);

        Builder = httpClientBuilder;
    }

    public void AuthorizeWithAccessToken<TAccessTokenProvider>() where TAccessTokenProvider : class, IHttpClientAccessTokenProvider
    {
        IncludeAccessTokenHttpClientDelegatingHandler = true;

        Builder.Services.TryAddTransient<AccessTokenHttpClientDelegatingHandler>();
        Builder.Services.TryAddSingleton<IHttpClientAccessTokenProvider, TAccessTokenProvider>();
    }

    /// <exception cref="AuthorizedWithAccessTokenNotIncludedException"/>
    public void AutomaticallyRefreshAccessToken<TRefreshTokenProvider, TAccessTokenRefresher>() where TRefreshTokenProvider : class, IHttpClientRefreshTokenProvider where TAccessTokenRefresher : class, IHttpClientAccessTokenRefresher
    {
        if (!IncludeAccessTokenHttpClientDelegatingHandler)
        {
            throw new AuthorizedWithAccessTokenNotIncludedException();
        }

        IncludeRefreshTokenHttpClientDelegatingHandler = true;

        Builder.Services.TryAddTransient<RefreshTokenHttpClientDelegatingHandler>();
        Builder.Services.TryAddSingleton<IRefreshTokenService, RefreshTokenService>();
        Builder.Services.TryAddSingleton<IHttpClientRefreshTokenProvider, TRefreshTokenProvider>();
        Builder.Services.TryAddSingleton<IHttpClientAccessTokenRefresher, TAccessTokenRefresher>();
    }

    public void ForwardUnauthorizedRequests<TUnauthorizedListener>() where TUnauthorizedListener : class, IHttpClientUnauthorizedListener
    {
        IncludeUnauthorizedHttpClientDelegatingHandler = true;

        Builder.Services.TryAddTransient<UnauthorizedHttpClientDelegatingHandler>();
        Builder.Services.TryAddSingleton<IHttpClientUnauthorizedListener, TUnauthorizedListener>();
    }

    public void Build()
    {
        //the order here matters
        //which is why we use
        //the include booleans

        if (IncludeUnauthorizedHttpClientDelegatingHandler)
        {
            Builder.AddHttpMessageHandler<UnauthorizedHttpClientDelegatingHandler>();
        }

        if (IncludeRefreshTokenHttpClientDelegatingHandler)
        {
            Builder.AddHttpMessageHandler<RefreshTokenHttpClientDelegatingHandler>();
        }

        if (IncludeAccessTokenHttpClientDelegatingHandler)
        {
            Builder.AddHttpMessageHandler<AccessTokenHttpClientDelegatingHandler>();
        }
    }
}
