using Lexicom.Authentication.Http.Exceptions;
using System.Net;

namespace Lexicom.Authentication.Http.DelegatingHandlers;
public class UnauthorizedHttpClientDelegatingHandler : DelegatingHandler
{
    private readonly IHttpClientUnathorizedListener _httpClientUnathorizedService;

    /// <exception cref="ArgumentNullException"/>
    public UnauthorizedHttpClientDelegatingHandler(IHttpClientUnathorizedListener httpClientUnathorizedService)
    {
        ArgumentNullException.ThrowIfNull(httpClientUnathorizedService);

        _httpClientUnathorizedService = httpClientUnathorizedService;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="UnauthorizedException"/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is HttpStatusCode.Unauthorized)
        {
            await _httpClientUnathorizedService.UnathorizedAsync();

            throw new UnauthorizedException(request.Method, response.RequestMessage?.RequestUri?.ToString(), response.StatusCode);
        }

        return response;
    }
}
