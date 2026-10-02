using Lexicom.Authentication.Http.Services;
using System.Net;

namespace Lexicom.Authentication.Http.DelegatingHandlers;

public class RefreshTokenHttpClientDelegatingHandler : DelegatingHandler
{
    private readonly IRefreshTokenService _refreshTokenService;

    /// <exception cref="ArgumentNullException"/>
    public RefreshTokenHttpClientDelegatingHandler(IRefreshTokenService refreshTokenService)
    {
        ArgumentNullException.ThrowIfNull(refreshTokenService);

        _refreshTokenService = refreshTokenService;
    }

    /// <exception cref="ArgumentNullException"/>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        //buffer the request content before the first send so the exact same request can be
        //sent again after a token refresh; without this the first send consumes the content
        //(especially for non seekable stream content) and the retry would send an empty body
        if (request.Content is not null)
        {
            await request.Content.LoadIntoBufferAsync(cancellationToken);
        }

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is not HttpStatusCode.Unauthorized)
        {
            return response;
        }

        await _refreshTokenService.RefreshTokenAsync(cancellationToken);

        response.Dispose();

        return await base.SendAsync(request, cancellationToken);
    }
}
