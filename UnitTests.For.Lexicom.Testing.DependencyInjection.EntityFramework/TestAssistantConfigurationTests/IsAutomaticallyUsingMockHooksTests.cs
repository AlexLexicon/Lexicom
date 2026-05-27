using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.EntityFramework;
using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Databases;
using UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.Constructs.Services;
using Lexicom.Testing.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.EntityFramework.TestAssistantConfigurationTests;

public class IsAutomaticallyUsingMockHooksTests
{
    [Fact]
    public void Successfully_Make_When_Not_Automatically_Mocking_Using_Hooks_But_DbContextFactory_Is_Mocked_Using_Databaes()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsAutomaticallyUsingMockHooks = false,
        });

        uta.Database<PeopleDbContext>();

        //act
        var uot = uta.Make<PeopleService>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._dbContextFactory);

        Assert.False(uot.IsSubstitute());
        Assert.False(uot._dbContextFactory.IsSubstitute());
        Assert.True(uot._dbContextFactory is SqliteInMemoryTestDbContextFactory<PeopleDbContext>);
    }

    [Fact]
    public void Successfully_Make_When_Not_Automatically_Mocking_Using_Hooks_But_DbContextFactory_Is_Mocked_Using_Mock()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsAutomaticallyUsingMockHooks = false,
        });

        uta.Mock<IDbContextFactory<PeopleDbContext>>();

        //act
        var uot = uta.Make<PeopleService>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._dbContextFactory);

        Assert.False(uot.IsSubstitute());
        Assert.True(uot._dbContextFactory.IsSubstitute());
    }

    [Fact]
    public void Successfully_Make_When_Not_Automatically_Mocking_Using_Hooks_And_DbContextFactory_Is_Not_Mocked_Or_Manually_Provided()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsAutomaticallyUsingMockHooks = false,
        });

        //act
        var uot = uta.Make<PeopleService>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._dbContextFactory);

        Assert.False(uot.IsSubstitute());
        Assert.True(uot._dbContextFactory.IsSubstitute());
    }
}
