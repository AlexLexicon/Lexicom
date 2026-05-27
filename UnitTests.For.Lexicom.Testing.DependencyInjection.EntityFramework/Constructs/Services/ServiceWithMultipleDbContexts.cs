using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Databases;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Services;

public class ServiceWithMultipleDbContexts
{
    public readonly IDbContextFactory<ColorsDbContext> _colorsDbContextFactory;
    public readonly IDbContextFactory<PeopleDbContext> _peopleDbContextFactory;

    public ServiceWithMultipleDbContexts(
        IDbContextFactory<ColorsDbContext> colorsDbContextFactory,
        IDbContextFactory<PeopleDbContext> peopleDbContextFactory)
    {
        _colorsDbContextFactory = colorsDbContextFactory;
        _peopleDbContextFactory = peopleDbContextFactory;
    }

    public async Task RemoveEntityFirstFromDatabasesAsync()
    {
        await using var colorsDb = await _colorsDbContextFactory.CreateDbContextAsync();

        var firstColor = await colorsDb.Colors.FirstAsync();

        colorsDb.Colors.Remove(firstColor);

        await colorsDb.SaveChangesAsync();

        await using var peopleDb = await _peopleDbContextFactory.CreateDbContextAsync();

        var firstPerson = await peopleDb.People.FirstAsync();
        var firstHome = await peopleDb.Homes.FirstAsync();

        peopleDb.People.Remove(firstPerson);
        peopleDb.Homes.Remove(firstHome);

        await peopleDb.SaveChangesAsync();
    }
}
