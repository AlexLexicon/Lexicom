using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Claims;

namespace Lexicom.EntityFramework.Identity.Stores;
//this is a copy of the regular 'RoleStore' from Microsoft: https://source.dot.net/#Microsoft.AspNetCore.Identity.EntityFrameworkCore/RoleStore.cs
//but uses the IDbContextFactory in order to allow the async methods to be used in parallel
/// <exception cref="ArgumentNullException"/>
public class AsyncRoleStore<TRole>(IDbContextFactory<DbContext> contextFactory, IdentityErrorDescriber? describer = null) : AsyncRoleStore<TRole, DbContext, string>(contextFactory, describer) where TRole : IdentityRole<string>
{
}
/// <exception cref="ArgumentNullException"/>
public class AsyncRoleStore<TRole, TContext>(IDbContextFactory<TContext> contextFactory, IdentityErrorDescriber? describer = null) : AsyncRoleStore<TRole, TContext, string>(contextFactory, describer) where TRole : IdentityRole<string> where TContext : DbContext
{
}
/// <exception cref="ArgumentNullException"/>
public class AsyncRoleStore<TRole, TContext, TKey>(IDbContextFactory<TContext> contextFactory, IdentityErrorDescriber? describer = null) : AsyncRoleStore<TRole, TContext, TKey, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>>(contextFactory, describer), IQueryableRoleStore<TRole>, IRoleClaimStore<TRole> where TRole : IdentityRole<TKey> where TKey : IEquatable<TKey> where TContext : DbContext
{
}
public class AsyncRoleStore<TRole, TContext, TKey, TUserRole, TRoleClaim> : IQueryableRoleStore<TRole>, IRoleClaimStore<TRole>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
    where TContext : DbContext
    where TUserRole : IdentityUserRole<TKey>, new()
    where TRoleClaim : IdentityRoleClaim<TKey>, new()
{
    private bool _disposed;

    /// <exception cref="ArgumentNullException"/>
    public AsyncRoleStore(IDbContextFactory<TContext> contextFactory, IdentityErrorDescriber? describer = null)
    {
        ArgumentNullException.ThrowIfNull(contextFactory);

        ContextFactory = contextFactory;
        ErrorDescriber = describer ?? new IdentityErrorDescriber();
    }

    public virtual IDbContextFactory<TContext> ContextFactory { get; private set; }
    public IdentityErrorDescriber ErrorDescriber { get; set; }
    public bool AutoSaveChanges { get; set; } = true;
    public virtual IQueryable<TRole> Roles
    {
        get
        {
            using var db = ContextFactory.CreateDbContext();

            return db.Set<TRole>();
        }
    }

    protected virtual async Task SaveChanges(TContext context, CancellationToken cancellationToken = default)
    {
        if (AutoSaveChanges)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        await db.AddAsync(role, cancellationToken);

        await SaveChanges(db, cancellationToken);

        return IdentityResult.Success;
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        db.Attach(role);
        role.ConcurrencyStamp = Guid.NewGuid().ToString();
        db.Update(role);

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
    public virtual async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        db.Remove(role);

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
    public virtual Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);

        return Task.FromResult(ConvertIdToString(role.Id)!);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual Task<string?> GetRoleNameAsync(TRole role, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);

        return Task.FromResult(role.Name);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual Task SetRoleNameAsync(TRole role, string? roleName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);

        role.Name = roleName;

        return Task.CompletedTask;
    }

    public virtual TKey? ConvertIdFromString(string id)
    {
        if (id is null)
        {
            return default;
        }

        return (TKey?)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
    }

    public virtual string? ConvertIdToString(TKey id)
    {
        if (id.Equals(default))
        {
            return null;
        }

        return id.ToString();
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    public virtual async Task<TRole?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        var roleId = ConvertIdFromString(id);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TRole>()
            .FirstOrDefaultAsync(u => u.Id.Equals(roleId), cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    public virtual async Task<TRole?> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TRole>()
            .FirstOrDefaultAsync(r => r.NormalizedName == normalizedName, cancellationToken);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual Task<string?> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);

        return Task.FromResult(role.NormalizedName);
    }

    /// <exception cref="OperationCanceledException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual Task SetNormalizedRoleNameAsync(TRole role, string? normalizedName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);

        role.NormalizedName = normalizedName;

        return Task.CompletedTask;
    }

    protected void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public void Dispose() => _disposed = true;

    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual async Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        return await db
            .Set<TRoleClaim>()
            .Where(rc => rc.RoleId.Equals(role.Id))
            .Select(c => new Claim(c.ClaimType!, c.ClaimValue!))
            .ToListAsync(cancellationToken);
    }

    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual async Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);
        ArgumentNullException.ThrowIfNull(claim);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        await db
            .Set<TRoleClaim>()
            .AddAsync(CreateRoleClaim(role, claim), cancellationToken);

        await SaveChanges(db, cancellationToken);
    }

    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentNullException"/>
    public virtual async Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(role);
        ArgumentNullException.ThrowIfNull(claim);

        using var db = await ContextFactory.CreateDbContextAsync(cancellationToken);

        List<TRoleClaim> claims = await db
            .Set<TRoleClaim>()
            .Where(rc => rc.RoleId.Equals(role.Id) && rc.ClaimValue == claim.Value && rc.ClaimType == claim.Type)
            .ToListAsync(cancellationToken);

        foreach (TRoleClaim c in claims)
        {
            db
                .Set<TRoleClaim>()
                .Remove(c);
        }

        await SaveChanges(db, cancellationToken);
    }

    protected virtual TRoleClaim CreateRoleClaim(TRole role, Claim claim)
    {
        return new TRoleClaim
        {
            RoleId = role.Id,
            ClaimType = claim.Type,
            ClaimValue = claim.Value
        };
    }
}