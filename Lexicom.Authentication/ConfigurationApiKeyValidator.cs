using Lexicom.Authentication.Options;
using Lexicom.Jwt;
using Lexicom.Validation.Options;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lexicom.Authentication;

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

        ApiKeyValidationResult result;
        if (apiKeyOptions.Keys is null)
        {
            result = ApiKeyValidationResult.Invalid();

            return Task.FromResult(result);
        }

        byte[] providedApiKeyBytes = Encoding.UTF8.GetBytes(apiKey);

        ApiKeyOptionsKey? matchedKey = null;
        foreach (ApiKeyOptionsKey key in apiKeyOptions.Keys)
        {
            AbstractOptionsValidator<ApiKeyOptions>.ThrowIfNull(key.Key);

            byte[] descriptorApiKeyBytes = Encoding.UTF8.GetBytes(key.Key);

            //'FixedTimeEquals' helps protect against timing attacks it returns false right away when the lengths are different
            if (CryptographicOperations.FixedTimeEquals(providedApiKeyBytes, descriptorApiKeyBytes))
            {
                matchedKey = key;

                break;
            }
        }

        if (matchedKey is null)
        {
            result = ApiKeyValidationResult.Invalid();

            return Task.FromResult(result);
        }

        AbstractOptionsValidator<ApiKeyOptions>.ThrowIfNull(matchedKey.Id);

        var claims = new List<Claim>();

        if (matchedKey.Permissions is not null)
        {
            foreach (string permission in matchedKey.Permissions)
            {
                claims.Add(new Claim(LexicomJwtClaimTypes.Permission, permission));
            }
        }

        if (matchedKey.Roles is not null)
        {
            foreach (string role in matchedKey.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        if (matchedKey.Claims is not null)
        {
            foreach ((string type, string value) in matchedKey.Claims)
            {
                claims.Add(new Claim(type, value));
            }
        }

        result = ApiKeyValidationResult.Valid(matchedKey.Id.Value, claims);

        return Task.FromResult(result);
    }
}
