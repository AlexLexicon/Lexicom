using Lexicom.Testing.DependencyInjection.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Testing.DependencyInjection;

public static class UnitTestAssistantExtensions
{
    public static TDbContext Database<TDbContext>(this UnitTestAssistant unitTestAssistant) where TDbContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(unitTestAssistant);

        return unitTestAssistant.MockManager.MockDatabase<TDbContext>();
    }
}
