using Lexicom.Testing.DependencyInjection.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Testing.DependencyInjection.EntityFramework;

public class IntegrationTestAssistant<TDbContext> : IntegrationTestAssistant where TDbContext : DbContext
{
    public IntegrationTestAssistant() : this(new TestAssistantConfiguration())
    {
    }
    public IntegrationTestAssistant(TestAssistantConfiguration configuration) : base(configuration)
    {
        Database = this.Database<TDbContext>();
    }

    public TDbContext Database { get; }
}
