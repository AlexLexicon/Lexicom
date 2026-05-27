using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Databases;
using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Entities;
using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Services;
using Microsoft.EntityFrameworkCore;
using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.EntityFramework;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.UnitTestAssistantTests;

public class DatabaseTests
{
    private static Person GetNewBob(Guid? homeId = null) => GetNewPerson("bob", 32, homeId);
    private static Person GetNewAlice(Guid? homeId = null) => GetNewPerson("Alice", 30, homeId);
    private static Person GetNewAlex(Guid? homeId = null) => GetNewPerson("Alex", 29, homeId);
    private static Person GetNewPerson(string name, int age, Guid? homeId = null)
    {
        return new Person
        {
            Id = Guid.NewGuid(),
            HomeId = homeId is null ? Guid.NewGuid() : homeId.Value,
            Name = name,
            Age = age,
            CreatedDateTimeUtc = DateTimeOffset.UtcNow,
            ModifiedDateTimeUtc = DateTimeOffset.UtcNow,
        };
    }

    [Fact]
    public async Task Successfully_Arrange_Database_For_Read_Test()
    {
        //arrange
        using var uta = new UnitTestAssistant<PeopleDbContext>();

        var bob = GetNewBob();
        var alice = GetNewAlice();
        var alex = GetNewAlex();

        uta.Database.People.Add(bob);
        uta.Database.People.Add(alice);
        uta.Database.People.Add(alex);

        await uta.Database.SaveChangesAsync(TestContext.Current.CancellationToken);

        //act
        var uot = uta.Make<PeopleService>();

        int countOfPeopleWithAgesGreaterThanOrEqualTo30 = await uot.CountOfPeopleWithAgesGreaterThanOrEqualTo30Async();

        //assert
        Assert.Equal(2, countOfPeopleWithAgesGreaterThanOrEqualTo30);
    }

    [Fact]
    public async Task Successfully_Arrange_Database_For_Write_Test()
    {
        //arrange
        using var uta = new UnitTestAssistant<PeopleDbContext>();

        //act
        var uot = uta.Make<PeopleService>();

        await uot.AddPeople();

        int count = await uta.Database.People.CountAsync(TestContext.Current.CancellationToken);

        //assert
        Assert.Equal(3, count);
    }

    [Fact]
    public async Task Successfully_Arrange_Database_For_Read_And_Write_Test()
    {
        //arrange
        using var uta = new UnitTestAssistant<PeopleDbContext>();

        var bob = GetNewBob();
        var alice = GetNewAlice();
        var alex = GetNewAlex();

        uta.Database.People.Add(bob);
        uta.Database.People.Add(alice);
        uta.Database.People.Add(alex);

        await uta.Database.SaveChangesAsync(TestContext.Current.CancellationToken);

        var uot = uta.Make<PeopleService>();

        //act
        int originalCount = await uot.GetCountOfPeopleThenRemoveThemAllAsync();

        int newCount = await uta.Database.People.CountAsync(TestContext.Current.CancellationToken);

        //assert
        Assert.Equal(3, originalCount);
        Assert.Equal(0, newCount);
    }

    [Fact]
    public async Task Successfully_Arrange_Multiple_Databases()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        var colorsDb = uta.Database<ColorsDbContext>();

        var red = new Color
        {
            Id = Guid.NewGuid(),
            Name = "red",
        };
        var blue = new Color
        {
            Id = Guid.NewGuid(),
            Name = "blue",
        };
        var yellow = new Color
        {
            Id = Guid.NewGuid(),
            Name = "yellow",
        };
        var green = new Color
        {
            Id = Guid.NewGuid(),
            Name = "green",
        };

        colorsDb.Colors.Add(red);
        colorsDb.Colors.Add(blue);
        colorsDb.Colors.Add(yellow);
        colorsDb.Colors.Add(green);

        await colorsDb.SaveChangesAsync(TestContext.Current.CancellationToken);

        var peopleDb = uta.Database<PeopleDbContext>();

        var bobAndAliceHome = new Home
        {
            Id = new Guid(),
            Address = "123 new way rd",
            CreatedDateTimeUtc = DateTimeOffset.UtcNow,
            ModifiedDateTimeUtc = DateTimeOffset.UtcNow,
        };
        var alexHome = new Home
        {
            Id = new Guid(),
            Address = "5 rd",
            CreatedDateTimeUtc = DateTimeOffset.UtcNow,
            ModifiedDateTimeUtc = DateTimeOffset.UtcNow,
        };

        peopleDb.Homes.Add(bobAndAliceHome);
        peopleDb.Homes.Add(alexHome);

        var bob = GetNewBob(bobAndAliceHome.Id);
        var alice = GetNewAlice(bobAndAliceHome.Id);
        var alex = GetNewAlex(alexHome.Id);

        peopleDb.People.Add(bob);
        peopleDb.People.Add(alice);
        peopleDb.People.Add(alex);

        await peopleDb.SaveChangesAsync(TestContext.Current.CancellationToken);

        var uot = uta.Make<ServiceWithMultipleDbContexts>();

        //act
        await uot.RemoveEntityFirstFromDatabasesAsync();

        int colorsCount = await colorsDb.Colors.CountAsync(TestContext.Current.CancellationToken);
        int peopleCount = await peopleDb.People.CountAsync(TestContext.Current.CancellationToken);
        int homesCount = await peopleDb.Homes.CountAsync(TestContext.Current.CancellationToken);

        //assert
        Assert.Equal(3, colorsCount);
        Assert.Equal(2, peopleCount);
        Assert.Equal(1, homesCount);
    }

    [Fact]
    public void Automatically_Inject_Database_Dependencies()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        //act
        var uot = uta.Make<ServiceWithMultipleDbContexts>();

        //assert
        Assert.NotNull(uot._colorsDbContextFactory);
        Assert.NotNull(uot._peopleDbContextFactory);

        Assert.True(uot._colorsDbContextFactory is SqliteInMemoryTestDbContextFactory<ColorsDbContext>);
        Assert.True(uot._peopleDbContextFactory is SqliteInMemoryTestDbContextFactory<PeopleDbContext>);
    }
}
