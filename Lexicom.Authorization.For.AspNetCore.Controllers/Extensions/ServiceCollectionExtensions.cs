using Lexicom.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authorization.AspNetCore.Controllers.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAspNetCoreControllersAuthorizationPermissions(this IServiceCollection services, params string[] permissions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(permissions);

        return AddLexicomAspNetCoreControllersAuthorizationPermissions(services, permissions.AsEnumerable());
    }
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAspNetCoreControllersAuthorizationPermissions(this IServiceCollection services, IEnumerable<string> permissions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(permissions);

        if (permissions is not null)
        {
            //this allows AspNetCore APIs to use
            //the [Authorize(Policy = "MyPermission")] attribute
            services.Configure<AuthorizationOptions>(options =>
            {
                foreach (string permission in permissions)
                {
                    options.AddPolicy(permission, policy =>
                    {
                        policy.RequireClaim(LexicomJwtClaimTypes.Permission, permission);
                    });
                }
            });
        }

        return services;
    }
}
