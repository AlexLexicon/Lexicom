using Lexicom.EntityFramework.Identity.Extensions;
using Lexicom.Supports.ConsoleApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lexicom.EntityFramework.Identity.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddEntityFrameworkIdentity<TDbContext>(this IConsoleAppServiceBuilder builder) where TDbContext : IdentityDbContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomEntityFrameworkIdentity<TDbContext>();

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey>(this IConsoleAppServiceBuilder builder, EntityFrameworkIdentitySettings? settings = null)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey>(settings);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole>(this IConsoleAppServiceBuilder builder, EntityFrameworkIdentitySettings? settings = null)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, TUserRole, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole>(settings);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken>(this IConsoleAppServiceBuilder builder, EntityFrameworkIdentitySettings? settings = null)
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

        builder.ConsoleApplicationBuilder.Services.AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken>(settings);

        return builder;
    }
}
