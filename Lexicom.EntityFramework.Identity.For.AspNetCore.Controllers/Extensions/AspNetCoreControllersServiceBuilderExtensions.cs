using Lexicom.EntityFramework.Identity.Extensions;
using Lexicom.Supports.AspNetCore.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lexicom.EntityFramework.Identity.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddEntityFrameworkIdentity<TDbContext>(this IAspNetCoreControllersServiceBuilder builder, Action<IdentityOptions>? configure = null) where TDbContext : IdentityDbContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomEntityFrameworkIdentity<TDbContext>(configure);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey>(this IAspNetCoreControllersServiceBuilder builder, Action<IdentityOptions>? configure = null)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey>(configure);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole>(this IAspNetCoreControllersServiceBuilder builder, Action<IdentityOptions>? configure = null)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, TUserRole, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole>(configure);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken>(this IAspNetCoreControllersServiceBuilder builder, Action<IdentityOptions>? configure = null)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
        where TUserClaim : IdentityUserClaim<TKey>, new()
        where TUserLogin : IdentityUserLogin<TKey>, new()
        where TRoleClaim : IdentityRoleClaim<TKey>, new()
        where TUserToken : IdentityUserToken<TKey>, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken>(configure);

        return builder;
    }
}
