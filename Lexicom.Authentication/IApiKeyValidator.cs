using Lexicom.Validation.Options.Exceptions;

namespace Lexicom.Authentication;

public interface IApiKeyValidator
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NotValidatedOnStartupException{T}"/>
    Task<ApiKeyValidationResult> ValidateAsync(string apiKey, CancellationToken cancellationToken = default);
}
