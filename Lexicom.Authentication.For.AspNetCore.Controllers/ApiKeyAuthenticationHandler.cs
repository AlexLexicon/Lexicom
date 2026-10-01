using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Lexicom.Authentication.For.AspNetCore.Controllers;

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
        string authorizationHeader = Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            //there is no authorization header so this scheme cannot authenticate the request
            //returning 'NoResult' (rather than 'Fail') allows other schemes (eg bearer) to
            //still run when this scheme is stacked alongside them on the same endpoint
            return AuthenticateResult.NoResult();
        }

        if (!AuthenticationHeaderValue.TryParse(authorizationHeader, out AuthenticationHeaderValue? authenticationHeaderValue))
        {
            return AuthenticateResult.NoResult();
        }

        if (!string.Equals(authenticationHeaderValue.Scheme, Options.HeaderScheme, StringComparison.OrdinalIgnoreCase))
        {
            //a different authorization scheme was used (eg 'Bearer') so let the
            //handler for that scheme deal with the request
            return AuthenticateResult.NoResult();
        }

        string? apiKey = authenticationHeaderValue.Parameter;

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return AuthenticateResult.Fail("An api key was not provided in the authorization header.");
        }

        ApiKeyValidationResult validationResult = await _apiKeyValidator.ValidateAsync(apiKey, Context.RequestAborted);

        if (!validationResult.IsValid)
        {
            return AuthenticateResult.Fail("The provided api key is not valid.");
        }

        var claimsIdentity = new ClaimsIdentity(validationResult.Claims, Scheme.Name);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authenticationTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

        return AuthenticateResult.Success(authenticationTicket);
    }
}
