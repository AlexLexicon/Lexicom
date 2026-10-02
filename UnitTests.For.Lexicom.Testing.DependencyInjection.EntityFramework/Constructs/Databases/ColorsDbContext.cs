using Microsoft.EntityFrameworkCore;
using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Entities;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Databases;

public class ColorsDbContext : DbContext
{
    public ColorsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Color> Colors { get; set; }
}
