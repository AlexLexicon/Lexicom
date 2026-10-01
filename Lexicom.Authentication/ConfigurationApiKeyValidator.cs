using Lexicom.Authentication.Options;
using Lexicom.Jwt;
using Lexicom.Validation.Options;
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

        if (apiKeyOptions.Keys is null)
        {
            return Task.FromResult(ApiKeyValidationResult.Invalid());
        }

        byte[] providedApiKeyBytes = Encoding.UTF8.GetBytes(apiKey);

        ApiKeyOptionsDescriptor? matchedDescriptor = null;
        foreach (ApiKeyOptionsDescriptor descriptor in apiKeyOptions.Keys)
        {
            AbstractOptionsValidator<ApiKeyOptions>.ThrowIfNull(descriptor.Key);

            byte[] descriptorApiKeyBytes = Encoding.UTF8.GetBytes(descriptor.Key);

            //'FixedTimeEquals' helps protect against timing attacks
            //it returns false right away when the lengths are different
            if (CryptographicOperations.FixedTimeEquals(providedApiKeyBytes, descriptorApiKeyBytes))
            {
                matchedDescriptor = descriptor;
                break;
            }
        }

        if (matchedDescriptor is null)
        {
            return Task.FromResult(ApiKeyValidationResult.Invalid());
        }

        AbstractOptionsValidator<ApiKeyOptions>.ThrowIfNull(matchedDescriptor.Id);

        var claims = new List<Claim>();

        if (matchedDescriptor.Permissions is not null)
        {
            foreach (string permission in matchedDescriptor.Permissions)
            {
                claims.Add(new Claim(LexicomJwtClaimTypes.Permission, permission));
            }
        }

        if (matchedDescriptor.Roles is not null)
        {
            foreach (string role in matchedDescriptor.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        if (matchedDescriptor.Claims is not null)
        {
            foreach ((string type, string value) in matchedDescriptor.Claims)
            {
                claims.Add(new Claim(type, value));
            }
        }

        return Task.FromResult(ApiKeyValidationResult.Valid(matchedDescriptor.Id.Value, claims));
    }
}
