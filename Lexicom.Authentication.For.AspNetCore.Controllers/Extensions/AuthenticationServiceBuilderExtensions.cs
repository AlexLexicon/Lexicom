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
}
