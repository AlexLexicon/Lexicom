using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Databases;

public class PeopleDbContext : DbContext
{
    public PeopleDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Home> Homes { get; set; }
}
