using System.Net;

namespace Lexicom.Authentication.Http.DelegatingHandlers;
public class RefreshTokenHttpClientDelegatingHandler : DelegatingHandler
{
    private readonly IHttpClientAccessTokenProvider _httpClientAccessTokenProvider;
    private readonly IHttpClientRefreshTokenProvider _httpClientRefreshTokenProvider;
    private readonly IHttpClientAccessTokenRefresher _httpClientRefreshService;

    /// <exception cref="ArgumentNullException"/>
    public RefreshTokenHttpClientDelegatingHandler(
        IHttpClientAccessTokenProvider accessTokenHttpClientProvider,
        IHttpClientRefreshTokenProvider refreshTokenHttpClientProvider,
        IHttpClientAccessTokenRefresher refreshService)
    {
        ArgumentNullException.ThrowIfNull(accessTokenHttpClientProvider);
        ArgumentNullException.ThrowIfNull(refreshTokenHttpClientProvider);
        ArgumentNullException.ThrowIfNull(refreshService);

        _httpClientAccessTokenProvider = accessTokenHttpClientProvider;
        _httpClientRefreshTokenProvider = refreshTokenHttpClientProvider;
        _httpClientRefreshService = refreshService;
    }

    /// <exception cref="ArgumentNullException"/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await SendAsync(request, isRefreshed: false, cancellationToken);
    }

    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, bool isRefreshed, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is not HttpStatusCode.Unauthorized || isRefreshed)
        {
            return response;
        }

        var getAccessTokenTask = _httpClientAccessTokenProvider.GetAccessTokenAsync();
        var getRefreshTokenAsync = _httpClientRefreshTokenProvider.GetRefreshTokenAsync();

        string? accessToken = await getAccessTokenTask;
        string? refreshToken = await getRefreshTokenAsync;

        await _httpClientRefreshService.RefreshAuthenticationAsync(accessToken, refreshToken);

        return await SendAsync(request, isRefreshed: true, cancellationToken);
    }
}
