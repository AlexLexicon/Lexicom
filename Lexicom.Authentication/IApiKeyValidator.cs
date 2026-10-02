namespace Lexicom.Authentication;

public interface IApiKeyValidator
{
    /// <exception cref="ArgumentNullException"/>
    Task<ApiKeyValidationResult> ValidateAsync(string apiKey, CancellationToken cancellationToken = default);
}
