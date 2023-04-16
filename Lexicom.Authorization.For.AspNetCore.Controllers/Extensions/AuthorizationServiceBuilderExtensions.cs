namespace Lexicom.Authorization.AspNetCore.Controllers.Extensions;
public static class AuthorizationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAuthorizationServiceBuilder AddPermissions(this IAuthorizationServiceBuilder builder, params string[] permissions)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(permissions);

        builder.Services.AddLexicomAspNetCoreControllersAuthorizationPermissions(permissions);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IAuthorizationServiceBuilder AddPermissions(this IAuthorizationServiceBuilder builder, IEnumerable<string> permissions)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(permissions);

        builder.Services.AddLexicomAspNetCoreControllersAuthorizationPermissions(permissions);

        return builder;
    }
}
