using Lexicom.EntityFramework.Identity.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.EntityFramework.Identity.Extensions;
//based on Microsoft source code: https://source.dot.net/#Microsoft.AspNetCore.Identity.EntityFrameworkCore/IdentityEntityFrameworkBuilderExtensions.cs,75eafbaa1f0ec288
public static class IdentityBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public static IdentityBuilder AddAsyncEntityFrameworkStores<TContext>(this IdentityBuilder builder) where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        AddAsyncStores(builder.Services, builder.UserType, builder.RoleType, typeof(TContext));

        return builder;
    }

    private static void AddAsyncStores(IServiceCollection services, Type userType, Type? roleType, Type contextType)
    {
        Type? identityUserType = FindGenericBaseType(userType, typeof(IdentityUser<>));
        if (identityUserType is null)
        {
            throw new InvalidOperationException("AddEntityFrameworkStores can only be called with a user that derives from IdentityUser&lt;TKey&gt;.");
        }

        Type keyType = identityUserType.GenericTypeArguments[0];

        if (roleType is not null)
        {
            Type? identityRoleType = FindGenericBaseType(roleType, typeof(IdentityRole<>));
            if (identityRoleType is null)
            {
                throw new InvalidOperationException("AddEntityFrameworkStores can only be called with a role that derives from IdentityRole&lt;TKey&gt;.");
            }

            Type userStoreType;
            Type roleStoreType;
            Type? identityContext = FindGenericBaseType(contextType, typeof(IdentityDbContext<,,,,,,,>));
            if (identityContext is null)
            {
                //if its a custom DbContext, we can only add the default POCOs
                userStoreType = typeof(AsyncUserStore<,,,>).MakeGenericType(userType, roleType, contextType, keyType);
                roleStoreType = typeof(AsyncRoleStore<,,>).MakeGenericType(roleType, contextType, keyType);
            }
            else
            {
                userStoreType = typeof(AsyncUserStore<,,,,,,,,>).MakeGenericType(
                    userType, 
                    roleType, 
                    contextType,
                    identityContext.GenericTypeArguments[2],
                    identityContext.GenericTypeArguments[3],
                    identityContext.GenericTypeArguments[4],
                    identityContext.GenericTypeArguments[5],
                    identityContext.GenericTypeArguments[7],
                    identityContext.GenericTypeArguments[6]);

                roleStoreType = typeof(AsyncRoleStore<,,,,>).MakeGenericType(
                    roleType, 
                    contextType,
                    identityContext.GenericTypeArguments[2],
                    identityContext.GenericTypeArguments[4],
                    identityContext.GenericTypeArguments[6]);
            }

            services.TryAddScoped(typeof(IUserStore<>).MakeGenericType(userType), userStoreType);
            services.TryAddScoped(typeof(IRoleStore<>).MakeGenericType(roleType), roleStoreType);
        }
        else
        {   //no Roles
            Type userStoreType;
            var identityContext = FindGenericBaseType(contextType, typeof(IdentityUserContext<,,,,>));
            if (identityContext == null)
            {
                //if its a custom DbContext, we can only add the default POCOs
                userStoreType = typeof(UserOnlyStore<,,>).MakeGenericType(userType, contextType, keyType);
            }
            else
            {
                userStoreType = typeof(UserOnlyStore<,,,,,>).MakeGenericType(userType, contextType,
                    identityContext.GenericTypeArguments[1],
                    identityContext.GenericTypeArguments[2],
                    identityContext.GenericTypeArguments[3],
                    identityContext.GenericTypeArguments[4]);
            }

            services.TryAddScoped(typeof(IUserStore<>).MakeGenericType(userType), userStoreType);
        }
    }

    private static Type? FindGenericBaseType(Type currentType, Type genericBaseType)
    {
        Type? type = currentType;
        while (type is not null)
        {
            Type? genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
            if (genericType is not null && genericType == genericBaseType)
            {
                return type;
            }

            type = type.BaseType;
        }

        return null;
    }
}
