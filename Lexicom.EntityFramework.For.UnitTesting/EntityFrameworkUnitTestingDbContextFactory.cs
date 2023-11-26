using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Lexicom.EntityFramework.For.UnitTesting;
//the purpose of this forwarded 'IDbContextFactory'
//is to ensure the database is created implicitly
public class EntityFrameworkUnitTestingDbContextFactory<TContext> : IDbContextFactory<TContext> where TContext : DbContext
{
#pragma warning disable CA1859 // Use concrete types when possible for improved performance
    private readonly IDbContextFactory<TContext> _dbContextFactory;
#pragma warning restore CA1859 // Use concrete types when possible for improved performance

    /// <exception cref="ArgumentNullException"/>
#pragma warning disable EF1001 // Internal EF Core API usage.
    public EntityFrameworkUnitTestingDbContextFactory(DbContextFactory<TContext> dbContextFactory)
#pragma warning restore EF1001 // Internal EF Core API usage.
    {
        ArgumentNullException.ThrowIfNull(dbContextFactory);

        _dbContextFactory = dbContextFactory;
    }

    public TContext CreateDbContext()
    {
        var db = _dbContextFactory.CreateDbContext();

        db.Database.EnsureCreated();

        return db;
    }

    public async Task<TContext> CreateDbContextAsync(CancellationToken cancellationToken = default)
    {
        var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        await db.Database.EnsureCreatedAsync(cancellationToken);

        return db;
    }
}
