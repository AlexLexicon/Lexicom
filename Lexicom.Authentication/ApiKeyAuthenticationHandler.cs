using Lexicom.Authentication.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Lexicom.Authentication;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    private readonly IApiKeyValidator _apiKeyValidator;

    /// <exception cref="ArgumentNullException"/>
    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IApiKeyValidator apiKeyValidator) : base(options, logger, encoder)
    {
        ArgumentNullException.ThrowIfNull(apiKeyValidator);

        _apiKeyValidator = apiKeyValidator;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.TryGetApiKey(Options.HeaderScheme, out string? apiKey))
        {
            //there is no api key authorization header so this scheme cannot authenticate the request
            //returning 'NoResult' (rather than 'Fail') allows other schemes (eg bearer) to
            //still run when this scheme is stacked alongside them on the same endpoint
            return AuthenticateResult.NoResult();
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return AuthenticateResult.Fail("An api key was not provided in the authorization header.");
        }

        ApiKeyValidationResult validationResult = await _apiKeyValidator.ValidateAsync(apiKey, Context.RequestAborted);

        if (!validationResult.IsValid)
        {
            return AuthenticateResult.Fail("The provided api key is not valid.");
        }

        //the 'sub' claim always comes from the validated api key id so it
        //cannot be overridden by any other claims provided by the validator
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, validationResult.Id.ToString()),
        };

        claims.AddRange(validationResult.Claims.Where(c => c.Type != JwtRegisteredClaimNames.Sub));

        var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authenticationTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

        return AuthenticateResult.Success(authenticationTicket);
    }
}
