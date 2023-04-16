using Lexicom.Jwt.Extensions;
using Lexicom.Jwt.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authority.Extensions;
public static class AuthorityServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAuthorityServiceBuilder AddAccessTokenProvider(this IAuthorityServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddJwtSecretsOptions(JwtOptions.ACCESS_TOKEN_SECTION);

        builder.Services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAuthorityServiceBuilder AddRefreshTokenProvider(this IAuthorityServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddJwtSecretsOptions(JwtOptions.REFRESH_TOKEN_SECTION);

        builder.Services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();

        return builder;
    }
}
