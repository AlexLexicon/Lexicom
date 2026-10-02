using Microsoft.EntityFrameworkCore;

namespace Lexicom.Testing.DependencyInjection.EntityFramework.Extensions;

public static class IntegrationTestAssistantExtensions
{
    public static TDbContext Database<TDbContext>(this IntegrationTestAssistant integrationTestAssistant) where TDbContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(integrationTestAssistant);

        return integrationTestAssistant.MockManager.MockDatabase<TDbContext>();
    }
}
