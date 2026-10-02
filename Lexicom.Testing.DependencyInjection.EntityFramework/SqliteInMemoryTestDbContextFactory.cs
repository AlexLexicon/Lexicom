using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Testing.DependencyInjection.EntityFramework;

public class SqliteInMemoryTestDbContextFactory
{
    private IServiceCollection? DbContextFactoryServices { get; set; }
    private IServiceProvider? DbContextFactoryProvider { get; set; }
    public IDbContextFactory<TContext> GetActualDbContextFactory<TContext>() where TContext : DbContext
    {
        if (DbContextFactoryProvider is not null)
        {
            var actualDbContextFactory = DbContextFactoryProvider.GetService<IDbContextFactory<TContext>>();

            if (actualDbContextFactory is not null)
            {
                return actualDbContextFactory;
            }
        }

        DbContextFactoryServices ??= new ServiceCollection();

        DbContextFactoryServices.AddDbContextFactory<TContext>(options =>
        {
            string uniqueString = Guid
                .NewGuid()
                .ToString()
                .Replace("-", string.Empty)
                .ToLowerInvariant();

            //we create a unique in memory sqlite database so that relational implementations work
            string connectionString = $"DataSource=file:mb{uniqueString}?mode=memory&cache=shared";

            options.UseSqlite(connectionString);
        }, ServiceLifetime.Singleton);

        DbContextFactoryProvider = DbContextFactoryServices.BuildServiceProvider();

        return GetActualDbContextFactory<TContext>();
    }
}
public class SqliteInMemoryTestDbContextFactory<TContext> : SqliteInMemoryTestDbContextFactory, IDbContextFactory<TContext> where TContext : DbContext
{

    private readonly IDbContextFactory<TContext> _actualDbContextFactory;

    public SqliteInMemoryTestDbContextFactory()
    {
        _actualDbContextFactory = GetActualDbContextFactory<TContext>();
    }

    public TContext CreateDbContext()
    {
        var db = _actualDbContextFactory.CreateDbContext();

        db.Database.EnsureCreated();

        return db;
    }

    public async Task<TContext> CreateDbContextAsync(CancellationToken cancellationToken = default)
    {
        var db = await _actualDbContextFactory.CreateDbContextAsync(cancellationToken);

        await db.Database.EnsureCreatedAsync(cancellationToken);

        return db;
    }
}
