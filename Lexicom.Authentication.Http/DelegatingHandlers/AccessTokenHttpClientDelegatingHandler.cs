using System.Net.Http.Headers;

namespace Lexicom.Authentication.Http.DelegatingHandlers;
public class AccessTokenHttpClientDelegatingHandler : DelegatingHandler
{
    private readonly IHttpClientAccessTokenProvider _httpClientAccessTokenProvider;

    /// <exception cref="ArgumentNullException"/>
    public AccessTokenHttpClientDelegatingHandler(IHttpClientAccessTokenProvider httpClientAccessTokenProvider)
    {
        ArgumentNullException.ThrowIfNull(httpClientAccessTokenProvider);

        _httpClientAccessTokenProvider = httpClientAccessTokenProvider;
    }

    /// <exception cref="ArgumentNullException"/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        string? accessToken = await _httpClientAccessTokenProvider.GetAccessTokenAsync();

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
