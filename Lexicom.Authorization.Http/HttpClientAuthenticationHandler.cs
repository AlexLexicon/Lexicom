using System.Net.Http.Headers;

namespace Lexicom.Authorization.Http;
public class HttpClientAuthenticationHandler : DelegatingHandler
{
    private readonly IBearerTokenStorage _bearerTokenStorage;

    /// <exception cref="ArgumentNullException"/>
    public HttpClientAuthenticationHandler(IBearerTokenStorage bearerTokenStorage)
    {
        ArgumentNullException.ThrowIfNull(bearerTokenStorage);

        _bearerTokenStorage = bearerTokenStorage;
    }

    /// <exception cref="ArgumentNullException"/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        string? bearerToken = await _bearerTokenStorage.GetBearerTokenAsync();

        if (!string.IsNullOrWhiteSpace(bearerToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
