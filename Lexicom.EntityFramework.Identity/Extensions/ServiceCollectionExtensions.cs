using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.EntityFramework.Identity.Options;
using Lexicom.EntityFramework.Identity.Stores;
using Lexicom.EntityFramework.Identity.Validators;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Options.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.EntityFramework.Identity.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IdentityBuilder AddLexicomEntityFrameworkIdentity<TDbContext>(this IServiceCollection services, EntityFrameworkIdentitySettings? settings = null) where TDbContext : IdentityDbContext
    {
        ArgumentNullException.ThrowIfNull(services);

        settings ??= new EntityFrameworkIdentitySettings();

        AddIdentityCore<IdentityUser>(services, settings.SetupAction);

        var identityBuilder = new IdentityBuilder(typeof(IdentityUser), typeof(IdentityRole), services)
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddSignInManager<SignInManager<IdentityUser>>();

        if (settings.UseAsyncStores)
        {
            identityBuilder = identityBuilder.AddAsyncEntityFrameworkStores<TDbContext>();
        }
        else
        {
            identityBuilder = identityBuilder.AddEntityFrameworkStores<TDbContext>();
        }

        AddTokenProviders<IdentityUser>(services, identityBuilder);

        return identityBuilder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IdentityBuilder AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey>(this IServiceCollection services, EntityFrameworkIdentitySettings? settings = null)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        ArgumentNullException.ThrowIfNull(services);

        return AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, IdentityUserRole<TKey>, IdentityUserClaim<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>(services, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IdentityBuilder AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole>(this IServiceCollection services, EntityFrameworkIdentitySettings? settings = null)
        where TDbContext : IdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, TUserRole, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
    {
        ArgumentNullException.ThrowIfNull(services);

        return AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole, IdentityUserClaim<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>(services, settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IdentityBuilder AddLexicomEntityFrameworkIdentity<TDbContext, TUser, TRole, TKey, TUserRole, TUserClaim, TUserLogin, TRoleClaim, TUserToken>(this IServiceCollection services, EntityFrameworkIdentitySettings? settings = null)
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
        ArgumentNullException.ThrowIfNull(services);

        settings ??= new EntityFrameworkIdentitySettings();

        AddIdentityCore<TUser>(services, settings.SetupAction);

        var identityBuilder = new IdentityBuilder(typeof(TUser), typeof(TRole), services)
            .AddUserManager<UserManager<TUser>>()
            .AddRoles<TRole>()
            .AddSignInManager<SignInManager<TUser>>();

        if (settings.UseAsyncStores)
        {
            identityBuilder = identityBuilder
                .AddUserStore<AsyncUserStore<TUser, TRole, TDbContext, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim>>()
                .AddRoleStore<AsyncRoleStore<TRole, TDbContext, TKey, TUserRole, TRoleClaim>>();
        }
        else
        {
            identityBuilder = identityBuilder
                .AddUserStore<UserStore<TUser, TRole, TDbContext, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim>>()
                .AddRoleStore<RoleStore<TRole, TDbContext, TKey, TUserRole, TRoleClaim>>();
        }

        AddTokenProviders<TUser>(services, identityBuilder);

        return identityBuilder;
    }

    private static void AddIdentityCore<TUser>(IServiceCollection services, Action<IdentityOptions>? configure) where TUser : class
    {
        services.AddLexicomValidationAmenities();

        services.AddOptions<IdentityOptions>()
            .BindConfiguration()
            .Validate<IdentityOptions, IdentityOptionsValidator>()
            .ValidateOnStart();

        if (configure is not null)
        {
            services.AddIdentityCore<TUser>(configure);
        }
        else
        {
            services.AddIdentityCore<TUser>();
        }
    }

    private static void AddTokenProviders<TUser>(IServiceCollection services, IdentityBuilder identityBuilder) where TUser : class
    {
        services.AddDataProtection();

        services.AddOptions<ChangeEmailTokenProviderOptions>()
            .BindConfiguration()
            .Validate<ChangeEmailTokenProviderOptions, IdentityChangeEmailTokenProviderOptionsValidator>()
            .ValidateOnStart();

        services.AddOptions<EmailConfirmationTokenProviderOptions>()
            .BindConfiguration()
            .Validate<EmailConfirmationTokenProviderOptions, IdentityEmailConfirmationTokenProviderOptionsValidator>()
            .ValidateOnStart();

        services.AddOptions<PasswordResetTokenProviderOptions>()
            .BindConfiguration()
            .Validate<PasswordResetTokenProviderOptions, IdentityPasswordResetTokenProviderOptionsValidator>()
            .ValidateOnStart();

        services.AddOptions<DataProtectionTokenProviderOptions>()
            .BindConfiguration()
            .Validate<DataProtectionTokenProviderOptions, DataProtectionTokenProviderOptionsValidator>()
            .ValidateOnStart();

        identityBuilder
            .AddDefaultTokenProviders()
            .AddTokenProvider<IdentityChangeEmailTokenProvider<TUser>>(ChangeEmailTokenProviderOptions.NAME)
            .AddTokenProvider<IdentityEmailConfirmationTokenProvider<TUser>>(EmailConfirmationTokenProviderOptions.NAME)
            .AddTokenProvider<IdentityPasswordResetTokenProvider<TUser>>(PasswordResetTokenProviderOptions.NAME);
    }
}
