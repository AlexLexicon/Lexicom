using Lexicom.EntityFramework.For.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.EntityFramework.UnitTesting.Extensions;
public static class EntityFrameworkUnitTestingServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IEntityFrameworkUnitTestingServiceBuilder AddTestDataContext<TDbContext>(this IEntityFrameworkUnitTestingServiceBuilder builder, ServiceLifetime lifetime = ServiceLifetime.Singleton) where TDbContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddDbContextFactory<TDbContext>(options =>
        {
            string uniqueString = Guid
                .NewGuid()
                .ToString()
                .Replace("-", string.Empty)
                .ToLowerInvariant();

            //we create a in memory sqlite database so that relational implementations work
            string connectionString = $"DataSource=file:mb{uniqueString}?mode=memory&cache=shared";

            options.UseSqlite(connectionString);
        }, lifetime);

        //the following is needed in order to forward
        //the 'DbContextFactory<TDbContext>' type to the 'EntityFrameworkUnitTestingDbContextFactory<TDbContext>' type

#pragma warning disable EF1001 // Internal EF Core API usage.
        builder.Services.Add(new ServiceDescriptor(typeof(DbContextFactory<TDbContext>), typeof(DbContextFactory<TDbContext>), lifetime));
#pragma warning restore EF1001 // Internal EF Core API usage.

        builder.Services.Replace(new ServiceDescriptor(typeof(IDbContextFactory<TDbContext>), typeof(EntityFrameworkUnitTestingDbContextFactory<TDbContext>), lifetime));

        return builder;
    }
}
