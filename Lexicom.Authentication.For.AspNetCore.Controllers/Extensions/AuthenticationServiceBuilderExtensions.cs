using System.IdentityModel.Tokens.Jwt;

namespace Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
public static class AuthenticationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAuthenticationServiceBuilder AddAccessTokenAuthentication(this IAuthenticationServiceBuilder builder, Action<IAuthenticationAccessTokenBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomAspNetCoreControllersAuthenticationAccessTokenAuthentication(configure);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAuthenticationServiceBuilder ReplaceDefaultInboundClaimType(this IAuthenticationServiceBuilder builder, string claim)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(claim);

        ReplaceDefaultInboundClaimType(builder, claim, claim);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IAuthenticationServiceBuilder ReplaceDefaultInboundClaimType(this IAuthenticationServiceBuilder builder, string key, string value)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNullOrEmpty(value);

        if (JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.ContainsKey(key))
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[key] = value;
        }

        return builder;
    }
}
