using Lexicom.Authentication.Options;
using Lexicom.Jwt;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lexicom.Authentication;

//the configuration backed default 'IApiKeyValidator'
//it matches the provided api key against the keys configured in the 'ApiKeyOptions' section
public class ConfigurationApiKeyValidator : IApiKeyValidator
{
    private readonly IOptionsMonitor<ApiKeyOptions> _apiKeyOptions;

    /// <exception cref="ArgumentNullException"/>
    public ConfigurationApiKeyValidator(IOptionsMonitor<ApiKeyOptions> apiKeyOptions)
    {
        ArgumentNullException.ThrowIfNull(apiKeyOptions);

        _apiKeyOptions = apiKeyOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    public Task<ApiKeyValidationResult> ValidateAsync(string apiKey, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(apiKey);

        ApiKeyOptions apiKeyOptions = _apiKeyOptions.CurrentValue;

        byte[] providedApiKeyBytes = Encoding.UTF8.GetBytes(apiKey);

        ApiKeyOptionsDescriptor? matchedDescriptor = null;
        foreach (ApiKeyOptionsDescriptor descriptor in apiKeyOptions.KeyDescriptions)
        {
            if (descriptor.Key is null)
            {
                continue;
            }

            byte[] descriptorApiKeyBytes = Encoding.UTF8.GetBytes(descriptor.Key);

            //'FixedTimeEquals' helps protect against timing attacks
            //it requires both spans to be the same length
            if (providedApiKeyBytes.Length == descriptorApiKeyBytes.Length &&
                CryptographicOperations.FixedTimeEquals(providedApiKeyBytes, descriptorApiKeyBytes))
            {
                matchedDescriptor = descriptor;
                break;
            }
        }

        if (matchedDescriptor is null)
        {
            return Task.FromResult(ApiKeyValidationResult.Invalid());
        }

        var claims = new List<Claim>();

        foreach (string permission in matchedDescriptor.Permissions)
        {
            claims.Add(new Claim(LexicomJwtClaimTypes.Permission, permission));
        }

        return Task.FromResult(ApiKeyValidationResult.Valid(claims));
    }
}
