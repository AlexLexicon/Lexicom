namespace Lexicom.Authentication.Http;
public interface IHttpClientRefreshTokenProvider
{
    Task<string?> GetRefreshTokenAsync();
}
