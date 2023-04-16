using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.EntityFramework.UnitTesting.Extensions;
public static class EntityFrameworkUnitTestingServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IEntityFrameworkUnitTestingServiceBuilder AddTestDataContext<TDbContext>(this IEntityFrameworkUnitTestingServiceBuilder builder) where TDbContext : DbContext
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
        });

        return builder;
    }
}
