using Lexicom.Authentication.Http.Exceptions;
using System.Net;

namespace Lexicom.Authentication.Http.DelegatingHandlers;

public class UnauthorizedHttpClientDelegatingHandler : DelegatingHandler
{
    private readonly IHttpClientUnauthorizedListener _httpClientUnauthorizedListener;

    /// <exception cref="ArgumentNullException"/>
    public UnauthorizedHttpClientDelegatingHandler(IHttpClientUnauthorizedListener httpClientUnauthorizedListener)
    {
        ArgumentNullException.ThrowIfNull(httpClientUnauthorizedListener);

        _httpClientUnauthorizedListener = httpClientUnauthorizedListener;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="UnauthorizedException"/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is HttpStatusCode.Unauthorized)
        {
            await _httpClientUnauthorizedListener.UnauthorizedAsync();

            throw new UnauthorizedException(request.Method, response.RequestMessage?.RequestUri?.ToString(), response.StatusCode);
        }

        return response;
    }
}
