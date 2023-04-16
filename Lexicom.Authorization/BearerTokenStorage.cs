namespace Lexicom.Authorization;
public interface IBearerTokenStorage
{
    /// <exception cref="ArgumentNullException"/>
    Task SetBearerTokenAsync(string bearerToken);
    Task<string?> GetBearerTokenAsync();
}
