using Lexicom.Authentication.Http.DelegatingHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Authentication.Http;
/// <exception cref="ArgumentNullException"/>
public class AuthenticationHttpClientBuilder(IHttpClientBuilder httpClientBuilder)
{
    public IHttpClientBuilder Builder { get; } = httpClientBuilder;

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
