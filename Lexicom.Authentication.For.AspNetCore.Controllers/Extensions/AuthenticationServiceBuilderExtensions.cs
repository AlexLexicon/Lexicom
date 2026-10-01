using Lexicom.Authentication.Extensions;

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
    public static IAuthenticationServiceBuilder AddApiKeyAuthentication(this IAuthenticationServiceBuilder builder, Action<IApiKeyAuthenticationBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomApiKeyAuthentication(configure);

        return builder;
    }
}
