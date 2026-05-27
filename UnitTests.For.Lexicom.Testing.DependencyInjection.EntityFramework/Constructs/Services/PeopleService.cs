using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Databases;
using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Services;

public class PeopleService
{
    public readonly IDbContextFactory<PeopleDbContext> _dbContextFactory;

    public PeopleService(IDbContextFactory<PeopleDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<int> CountOfPeopleWithAgesGreaterThanOrEqualTo30Async()
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        return await db.People
            .Where(p => p.Age >= 30)
            .CountAsync();
    }

    public async Task AddPeople()
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        for (int count = 0; count < 3; count++)
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                HomeId = Guid.NewGuid(),
                Name = $"New Guy {count + 1}",
                Age = 25 + count,
                CreatedDateTimeUtc = DateTimeOffset.UtcNow,
                ModifiedDateTimeUtc = DateTimeOffset.UtcNow,
            };

            db.People.Add(person);

            await db.SaveChangesAsync();
        }
    }

    public async Task<int> GetCountOfPeopleThenRemoveThemAllAsync()
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        var people = await db.People.ToListAsync();

        foreach (Person person in people)
        {
            db.People.Remove(person);
        }

        await db.SaveChangesAsync();

        return people.Count;
    }
}
