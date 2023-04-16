using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authorization.Extensions;
public static class AuthorizationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAuthorizationServiceBuilder AddBearerTokenStorage<TBearerTokenStorage>(this IAuthorizationServiceBuilder builder) where TBearerTokenStorage : class, IBearerTokenStorage
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IBearerTokenStorage, TBearerTokenStorage>();

        return builder;
    }
}
