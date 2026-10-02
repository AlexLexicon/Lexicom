using Microsoft.EntityFrameworkCore;

namespace Lexicom.Testing.DependencyInjection;

public class UnitTestAssistant<TDbContext> : UnitTestAssistant where TDbContext : DbContext
{
    public UnitTestAssistant() : this(new TestAssistantConfiguration())
    {
    }
    public UnitTestAssistant(TestAssistantConfiguration configuration) : base(configuration)
    {
        Database = this.Database<TDbContext>();
    }

    public TDbContext Database { get; }
}
