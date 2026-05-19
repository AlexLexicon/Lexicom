namespace Lexicom.Authentication.For.ConsoleApp.Extensions;

public static class AuthenticationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAuthenticationServiceBuilder AddAccessTokenAuthentication(this IAuthenticationServiceBuilder builder, Action<IAuthenticationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomConsoleAppAuthenticationAccessTokenAuthentication(configure);

        return builder;
    }
}
