using Lexicom.Testing.DependencyInjection.Mocking;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Testing.DependencyInjection.EntityFramework.Extensions;

public static class MockManagerExtensions
{
    public static TDbContext MockDatabase<TDbContext>(this MockManager manager) where TDbContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(manager);

        Type dbContextFactoryType = typeof(IDbContextFactory<TDbContext>);

        IDbContextFactory<TDbContext> factory;
        if (manager.TryGetValue(dbContextFactoryType, out MockContainer? container))
        {
            factory = container.Pull<IDbContextFactory<TDbContext>>();
        }
        else
        {
            factory = new SqliteInMemoryTestDbContextFactory<TDbContext>();

            manager.Mock<IDbContextFactory<TDbContext>>(MockLifetime.Singleton).With(factory);
        }

        return factory.CreateDbContext();
    }
}
