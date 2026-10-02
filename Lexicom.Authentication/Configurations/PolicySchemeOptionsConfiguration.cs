using Lexicom.Authentication.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Lexicom.Authentication.Configurations;

public class PolicySchemeOptionsConfiguration : IConfigureNamedOptions<PolicySchemeOptions>
{
    private readonly IOptions<AuthenticationOptions> _authenticationOptions;
    private readonly IOptionsMonitor<ApiKeyAuthenticationOptions> _apiKeyAuthenticationOptions;

    /// <exception cref="ArgumentNullException"/>
    public PolicySchemeOptionsConfiguration(
        IOptions<AuthenticationOptions> authenticationOptions,
        IOptionsMonitor<ApiKeyAuthenticationOptions> apiKeyAuthenticationOptions)
    {
        ArgumentNullException.ThrowIfNull(authenticationOptions);
        ArgumentNullException.ThrowIfNull(apiKeyAuthenticationOptions);

        _authenticationOptions = authenticationOptions;
        _apiKeyAuthenticationOptions = apiKeyAuthenticationOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    public void Configure(string? name, PolicySchemeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (name is LexicomAuthenticationDefaults.AuthenticationScheme)
        {
            Configure(options);
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public void Configure(PolicySchemeOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.ForwardDefaultSelector = SelectScheme;
    }

    protected virtual string SelectScheme(HttpContext context)
    {
        IDictionary<string, AuthenticationSchemeBuilder> schemeMap = _authenticationOptions.Value.SchemeMap;

        bool isApiKeyRegistered = schemeMap.ContainsKey(ApiKeyDefaults.AuthenticationScheme);
        bool isBearerRegistered = schemeMap.ContainsKey(JwtBearerDefaults.AuthenticationScheme);

        if (isApiKeyRegistered)
        {
            ApiKeyAuthenticationOptions apiKeyAuthenticationOptions = _apiKeyAuthenticationOptions.Get(ApiKeyDefaults.AuthenticationScheme);

            //when only api key authentication is used it should also handle
            //requests without an authorization header so it can issue the challenge
            if (!isBearerRegistered || context.Request.TryGetApiKey(apiKeyAuthenticationOptions.HeaderScheme, out _))
            {
                return ApiKeyDefaults.AuthenticationScheme;
            }
        }

        return JwtBearerDefaults.AuthenticationScheme;
    }
}
