namespace Lexicom.Authentication.Http;
public interface IHttpClientAccessTokenProvider
{
    Task<string?> GetAccessTokenAsync();
}
