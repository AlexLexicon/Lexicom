using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Claims;

namespace Lexicom.EntityFramework.Identity.Stores;
//this is a copy of the regular 'RoleStore' from Microsoft: https://source.dot.net/#Microsoft.AspNetCore.Identity.EntityFrameworkCore/UserStore.cs
//but uses the IDbContextFactory in order to allow the async methods to be used in parallel
public class AsyncUserStore : AsyncUserStore<IdentityUser<string>>
{
    public AsyncUserStore(IDbContextFactory<DbContext> contextFactory, IdentityErrorDescriber? describer = null) : base(contextFactory, describer)
    {
    }
}
public class AsyncUserStore<TUser> : AsyncUserStore<TUser, IdentityRole, DbContext, string> where TUser : IdentityUser<string>, new()
{
    public AsyncUserStore(IDbContextFactory<DbContext> contextFactory, IdentityErrorDescriber? describer = null) : base(contextFactory, describer)
    {
    }
}
public class AsyncUserStore<TUser, TRole, TContext> : AsyncUserStore<TUser, TRole, TContext, string> where TUser : IdentityUser<string> where TRole : IdentityRole<string> where TContext : DbContext
{
    public AsyncUserStore(IDbContextFactory<TContext> contextFactory, IdentityErrorDescriber? describer = null) : base(contextFactory, describer)
    {
    }
}
public class AsyncUserStore<TUser, TRole, TContext, TKey> : AsyncUserStore<TUser, TRole, TContext, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityUserToken<TKey>, IdentityRoleClaim<TKey>>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
{
    public AsyncUserStore(IDbContextFactory<TContext> contextFactory, IdentityErrorDescriber? describer = null) : base(contextFactory, describer)
    {
    }
}
public class AsyncUserStore<TUser, TRole, TContext, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim> : UserStoreBase<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TUserToken, TRoleClaim>, IProtectedUserStore<TUser>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TContext : DbContext
    where TKey : IEquatable<TKey>
    where TUserClaim : IdentityUserClaim<TKey>, new()
    where TUserRole : IdentityUserRole<TKey>, new()
    where TUserLogin : IdentityUserLogin<TKey>, new()
    where TUserToken : IdentityUserToken<TKey>, new()
    where TRoleClaim : IdentityRoleClaim<TKey>, new()
{
    /// <exception cref="ArgumentNullException"/>
    public AsyncUserStore(IDbContextFactory<TContext> contextFactory, IdentityErrorDescriber? describer = null) : base(describer ?? new IdentityErrorDescriber())
    {
        ArgumentNullException.ThrowIfNull(contextFactory);

        ContextFactory = contextFactory;
    }

    public virtual IDbContextFactory<TContext> ContextFactory { get; private set; }

    public bool AutoSaveChanges { get; set; } = true;
    public override IQueryable<TUser> Users
    {
        get
        {
            using var db = ContextFactory.CreateDbContext();

            return db.Set<TUser>();
        }
    }

    protected async Task SaveChanges(TContext context, CancellationToken cancellationToken = default)
    {
        if (AutoSaveChanges)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        await db.AddAsync(user, cancellationToken);

        await SaveChanges(db, cancellationToken);

        return IdentityResult.Success;
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        db.Attach(user);
        user.ConcurrencyStamp = Guid.NewGuid().ToString();
        db.Update(user);

        try
        {
            await SaveChanges(db, cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
        }

        return IdentityResult.Success;
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        db.Remove(user);

        try
        {
            await SaveChanges(db, cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
        }

        return IdentityResult.Success;
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    public override async Task<TUser?> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        TKey? id = ConvertIdFromString(userId);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUser>()
            .FindAsync(new object?[]
            {
                id
            }, cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    public override async Task<TUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUser>()
            .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
    }

    protected override async Task<TRole?> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TRole>()
            .SingleOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);
    }

    protected override async Task<TUserRole?> FindUserRoleAsync(TKey userId, TKey roleId, CancellationToken cancellationToken)
    {
        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUserRole>()
            .FindAsync(new object[]
            {
                userId,
                roleId
            }, cancellationToken);
    }

    protected override async Task<TUser?> FindUserAsync(TKey userId, CancellationToken cancellationToken)
    {
        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUser>()
            .SingleOrDefaultAsync(u => u.Id.Equals(userId), cancellationToken);
    }

    protected override async Task<TUserLogin?> FindUserLoginAsync(TKey userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUserLogin>()
            .SingleOrDefaultAsync(userLogin => userLogin.UserId.Equals(userId) && userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey, cancellationToken);
    }

    protected override async Task<TUserLogin?> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUserLogin>()
            .SingleOrDefaultAsync(userLogin => userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey, cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="InvalidOperationException"/>
    public override async Task AddToRoleAsync(TUser user, string normalizedRoleName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        if (string.IsNullOrWhiteSpace(normalizedRoleName))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(normalizedRoleName));
        }

        TRole? roleEntity = await FindRoleAsync(normalizedRoleName, cancellationToken);
        if (roleEntity is null)
        {
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Role {0} does not exist.", normalizedRoleName));
        }

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        await db
            .Set<TUserRole>()
            .AddAsync(CreateUserRole(user, roleEntity), cancellationToken);

        await SaveChanges(db, cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentException"/>
    public override async Task RemoveFromRoleAsync(TUser user, string normalizedRoleName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        if (string.IsNullOrWhiteSpace(normalizedRoleName))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(normalizedRoleName));
        }

        TRole? roleEntity = await FindRoleAsync(normalizedRoleName, cancellationToken);
        if (roleEntity is not null)
        {
            TUserRole? userRole = await FindUserRoleAsync(user.Id, roleEntity.Id, cancellationToken);
            if (userRole is not null)
            {
                using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

                db
                    .Set<TUserRole>()
                    .Remove(userRole);

                await SaveChanges(db, cancellationToken);
            }
        }
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        TKey userId = user.Id;

        var query = from userRole in db.Set<TUserRole>()
                    join role in db.Set<TRole>() on userRole.RoleId equals role.Id
                    where userRole.UserId.Equals(userId)
                    select role.Name;

        return await query.ToListAsync(cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentException"/>
    public override async Task<bool> IsInRoleAsync(TUser user, string normalizedRoleName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        if (string.IsNullOrWhiteSpace(normalizedRoleName))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(normalizedRoleName));
        }

        TRole? role = await FindRoleAsync(normalizedRoleName, cancellationToken);
        if (role is not null)
        {
            TUserRole? userRole = await FindUserRoleAsync(user.Id, role.Id, cancellationToken);

            return userRole is not null;
        }

        return false;
    }

    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUserClaim>()
            .Where(uc => uc.UserId.Equals(user.Id))
            .Select(c => c.ToClaim())
            .ToListAsync(cancellationToken);
    }

    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(claims);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        foreach (Claim claim in claims)
        {
            await db
                .Set<TUserClaim>()
                .AddAsync(CreateUserClaim(user, claim), cancellationToken);
        }

        await SaveChanges(db, cancellationToken);
    }

    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(claim);
        ArgumentNullException.ThrowIfNull(newClaim);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        List<TUserClaim> matchedClaims = await db
            .Set<TUserClaim>()
            .Where(uc => uc.UserId.Equals(user.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type)
            .ToListAsync(cancellationToken);

        foreach (TUserClaim matchedClaim in matchedClaims)
        {
            matchedClaim.ClaimValue = newClaim.Value;
            matchedClaim.ClaimType = newClaim.Type;
        }

        await SaveChanges(db, cancellationToken);
    }

    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(claims);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        foreach (Claim claim in claims)
        {
            List<TUserClaim> matchedClaims = await db
                .Set<TUserClaim>()
                .Where(uc => uc.UserId.Equals(user.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type)
                .ToListAsync(cancellationToken);

            foreach (TUserClaim? c in matchedClaims)
            {
                db
                    .Set<TUserClaim>()
                    .Remove(c);
            }
        }

        await SaveChanges(db, cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(login);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        await db
            .Set<TUserLogin>()
            .AddAsync(CreateUserLogin(user, login), cancellationToken);

        await SaveChanges(db, cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        TUserLogin? entry = await FindUserLoginAsync(user.Id, loginProvider, providerKey, cancellationToken);
        if (entry is not null)
        {
            using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

            db
                .Set<TUserLogin>()
                .Remove(entry);

            await SaveChanges(db, cancellationToken);
        }
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(user);

        TKey userId = user.Id;

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUserLogin>()
            .Where(l => l.UserId.Equals(userId))
            .Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName))
            .ToListAsync(cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    public override async Task<TUser?> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        TUserLogin? userLogin = await FindUserLoginAsync(loginProvider, providerKey, cancellationToken);
        if (userLogin is not null)
        {
            return await FindUserAsync(userLogin.UserId, cancellationToken);
        }

        return null;
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    public override async Task<TUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUser>()
            .SingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public override async Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(claim);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        var query = from userclaims in db.Set<TUserClaim>()
                    join user in Users on userclaims.UserId equals user.Id
                    where userclaims.ClaimValue == claim.Value
                    && userclaims.ClaimType == claim.Type
                    select user;

        return await query.ToListAsync(cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentException"/>
    public override async Task<IList<TUser>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(normalizedRoleName);
        if (string.IsNullOrEmpty(normalizedRoleName))
        {
            throw new ArgumentException("Value cannot be empty.", nameof(normalizedRoleName));
        }

        TRole? role = await FindRoleAsync(normalizedRoleName, cancellationToken);

        if (role is not null)
        {
            using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

            var query = from userrole in db.Set<TUserRole>()
                        join user in Users on userrole.UserId equals user.Id
                        where userrole.RoleId.Equals(role.Id)
                        select user;

            return await query.ToListAsync(cancellationToken);
        }

        return new List<TUser>();
    }

    protected override async Task<TUserToken?> FindTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
    {
        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TUserToken>()
            .FindAsync(new object[]
            {
                user.Id,
                loginProvider,
                name
            }, cancellationToken);
    }

    protected override async Task AddUserTokenAsync(TUserToken token)
    {
        using var db = await ContextFactory.CreateDbContextAsync();

        await db
            .Set<TUserToken>()
            .AddAsync(token);

        await SaveChanges(db);
    }

    protected override async Task RemoveUserTokenAsync(TUserToken token)
    {
        using var db = await ContextFactory.CreateDbContextAsync();

        db
            .Set<TUserToken>()
            .Remove(token);

        await SaveChanges(db);
    }
}