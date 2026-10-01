using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Lexicom.Authentication.Configurations;

public class AuthenticationOptionsConfiguration : IConfigureOptions<AuthenticationOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public void Configure(AuthenticationOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        //both the access token and api key authentication register this configuration
        //so the policy scheme is only added once no matter which (or both) are used
        if (!options.SchemeMap.ContainsKey(LexicomAuthenticationDefaults.AuthenticationScheme))
        {
            options.AddScheme<PolicySchemeHandler>(LexicomAuthenticationDefaults.AuthenticationScheme, displayName: null);
        }

        //the policy scheme forwards to the api key or bearer scheme which allows the
        //[Authorize(Policy = "MyPermission")] attribute to work with either of them
        options.DefaultAuthenticateScheme = LexicomAuthenticationDefaults.AuthenticationScheme;
        options.DefaultScheme = LexicomAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = LexicomAuthenticationDefaults.AuthenticationScheme;
    }
}
