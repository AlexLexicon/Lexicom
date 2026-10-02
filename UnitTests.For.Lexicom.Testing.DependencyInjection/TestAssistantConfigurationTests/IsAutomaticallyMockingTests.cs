using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.Exceptions;
using Lexicom.Testing.DependencyInjection.Extensions;
using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;
using NSubstitute;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.TestAssistantConfigurationTests;

public class IsAutomaticallyMockingTests
{
    [Fact]
    public void Successfully_Make_When_Not_Automatically_Mocking_But_Dependency_Is_Mocked()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsAutomaticallyMocking = false,
        });

        uta.Mock<IServiceDependencyIntReturnMethod>();

        //act
        var uot = uta.Make<ServiceWithDependency>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._serviceDependencyIntReturnMethod);

        Assert.False(uot.IsSubstitute());
        Assert.True(uot._serviceDependencyIntReturnMethod.IsSubstitute());
    }

    [Fact]
    public void Successfully_Make_When_Not_Automatically_Mocking_But_All_Dependency_Is_Manually_Provided()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsAutomaticallyMocking = false,
        });

        var mockableService = new ServiceDependencyIntReturnMethod();

        //act
        var uot = uta.Make<ServiceWithDependency>(mockableService);

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._serviceDependencyIntReturnMethod);

        Assert.False(uot.IsSubstitute());
        Assert.False(uot._serviceDependencyIntReturnMethod.IsSubstitute());
    }

    [Fact]
    public void Successfully_Make_When_Not_Automatically_Mocking_But_All_Dependencies_Are_Mocked_Or_Manually_Provided()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsAutomaticallyMocking = false,
        });

        uta.Mock<IServiceDependencyVoidReturnMethod>();
        uta.Mock<IServiceDependencyIntReturnMethod>();

        var mockServiceDependencyStringReturnMethod = Substitute.For<IServiceDependencyStringReturnMethod>();
        var mockServiceDependencyReferenceTypeReturnMethod = Substitute.For<IServiceDependencyReferenceTypeReturnMethod>();

        //act
        var uot = uta.Make<ServiceWithInterfaceDependencies>(mockServiceDependencyStringReturnMethod, mockServiceDependencyReferenceTypeReturnMethod);

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._serviceDependencyVoidReturnMethod);
        Assert.NotNull(uot._serviceDependencyIntReturnMethod);
        Assert.NotNull(uot._serviceDependencyStringReturnMethod);
        Assert.NotNull(uot._serviceDependencyReferenceTypeReturnMethod);

        Assert.False(uot.IsSubstitute());
        Assert.True(uot._serviceDependencyVoidReturnMethod.IsSubstitute());
        Assert.True(uot._serviceDependencyIntReturnMethod.IsSubstitute());
        Assert.True(uot._serviceDependencyStringReturnMethod.IsSubstitute());
        Assert.True(uot._serviceDependencyReferenceTypeReturnMethod.IsSubstitute());
    }

    [Fact]
    public void Fail_Make_When_Not_Automatically_Mocking_And_Dependency_Is_Not_Mocked_Or_Manually_Provided()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            IsAutomaticallyMocking = false,
        });

        //assert
        Assert.Throws<PullNotMockedException>(() =>
        {
            //act
            var uot = uta.Make<ServiceWithDependency>();
        });
    }
}
