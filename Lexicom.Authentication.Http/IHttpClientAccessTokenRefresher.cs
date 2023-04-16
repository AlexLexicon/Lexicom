namespace Lexicom.Authentication.Http;
public interface IHttpClientAccessTokenRefresher
{
    Task RefreshAuthenticationAsync(string? accessToken, string? refreshToken);
}
