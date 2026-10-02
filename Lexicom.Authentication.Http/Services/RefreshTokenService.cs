namespace Lexicom.Authentication.Http.Services;

public interface IRefreshTokenService
{
    Task RefreshTokenAsync(CancellationToken cancellationToken = default);
}
public class RefreshTokenService : IRefreshTokenService
{
    private readonly IHttpClientAccessTokenProvider _httpClientAccessTokenProvider;
    private readonly IHttpClientRefreshTokenProvider _httpClientRefreshTokenProvider;
    private readonly IHttpClientAccessTokenRefresher _httpClientRefreshService;

    /// <exception cref="ArgumentNullException"/>
    public RefreshTokenService(
        IHttpClientAccessTokenProvider httpClientAccessTokenProvider,
        IHttpClientRefreshTokenProvider httpClientRefreshTokenProvider,
        IHttpClientAccessTokenRefresher httpClientRefreshService)
    {
        ArgumentNullException.ThrowIfNull(httpClientAccessTokenProvider);
        ArgumentNullException.ThrowIfNull(httpClientRefreshTokenProvider);
        ArgumentNullException.ThrowIfNull(httpClientRefreshService);

        _httpClientAccessTokenProvider = httpClientAccessTokenProvider;
        _httpClientRefreshTokenProvider = httpClientRefreshTokenProvider;
        _httpClientRefreshService = httpClientRefreshService;

        RefreshSemaphore = new SemaphoreSlim(1, 1);
    }

    protected SemaphoreSlim RefreshSemaphore { get; }

    public async Task RefreshTokenAsync(CancellationToken cancellationToken)
    {
        //if the access token has changed by the time we acquire the lock then another 
        //concurrent request already performed the refresh and we must not refresh again
        string? accessTokenBeforeRefresh = await _httpClientAccessTokenProvider.GetAccessTokenAsync();

        await RefreshSemaphore.WaitAsync(cancellationToken);
        try
        {
            string? currentAccessToken = await _httpClientAccessTokenProvider.GetAccessTokenAsync();

            if (currentAccessToken == accessTokenBeforeRefresh)
            {
                string? refreshToken = await _httpClientRefreshTokenProvider.GetRefreshTokenAsync();

                await _httpClientRefreshService.RefreshAuthenticationAsync(currentAccessToken, refreshToken);
            }
        }
        finally
        {
            RefreshSemaphore.Release();
        }
    }
}
