using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.EntityFramework.Identity.Extensions;
using Lexicom.EntityFramework.Identity.UnitTesting;
using Lexicom.Supports.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.EntityFramework.Identity.For.Testing.Extensions;

public static class TestingServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ITestingServiceBuilder AddLexicomEntityFrameworkIdentity<TDbContext>(this ITestingServiceBuilder builder, IdentityOptions? identityOptions = null) where TDbContext : IdentityDbContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        PreTestIdentityConfiguration(builder, identityOptions);

        builder.Services.AddLexicomEntityFrameworkIdentity<TDbContext>();

        PostTestIdentityConfiguration(builder);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static ITestingServiceBuilder AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey>(this ITestingServiceBuilder builder, IdentityOptions? identityOptions = null)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        ArgumentNullException.ThrowIfNull(builder);

        PreTestIdentityConfiguration(builder, identityOptions);

        builder.Services.AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey>();

        PostTestIdentityConfiguration(builder);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static ITestingServiceBuilder AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole>(this ITestingServiceBuilder builder, IdentityOptions? identityOptions = null)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, TUserRole, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        PreTestIdentityConfiguration(builder, identityOptions);

        builder.Services.AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole>();

        PostTestIdentityConfiguration(builder);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static ITestingServiceBuilder AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken>(this ITestingServiceBuilder builder, IdentityOptions? identityOptions = null)
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

        PreTestIdentityConfiguration(builder, identityOptions);

        builder.Services.AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken>();

        PostTestIdentityConfiguration(builder);

        return builder;
    }

    private static void PreTestIdentityConfiguration(ITestingServiceBuilder builder, IdentityOptions? identityOptions)
    {
        if (identityOptions is not null)
        {
            builder.Configuration.AddInMemoryCollection(identityOptions);
        }
    }

    private static void PostTestIdentityConfiguration(ITestingServiceBuilder builder)
    {
        builder.Services.TryAddSingleton<IHttpContextAccessor>(sp =>
        {
            return new TestHttpContextAccessor(sp);
        });

        builder.Services.AddAuthentication();
    }
}
