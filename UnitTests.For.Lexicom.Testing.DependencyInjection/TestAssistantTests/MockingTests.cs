using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;
using NSubstitute;
using Lexicom.Testing.DependencyInjection;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.TestAssistantTests;

public class MockingTests
{
    [Fact]
    public async Task Mock_So_Returns_Specific_Value()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        int expectedNumber = 123;
        int asyncExpectedNumber = 321;
        uta.Mock<IServiceDependencyIntReturnMethod>().So(ms =>
        {
            ms.GetValueTypeIntMethod().Returns(expectedNumber);
            ms.GetValueTypeIntMethodAsync().Returns(asyncExpectedNumber);
        });

        //act
        var uot = uta.Make<ServiceWithDependency>();

        int number = uot._serviceDependencyIntReturnMethod.GetValueTypeIntMethod();
        int asyncNumber = await uot._serviceDependencyIntReturnMethod.GetValueTypeIntMethodAsync();

        //assert
        Assert.Equal(expectedNumber, number);
        Assert.Equal(asyncExpectedNumber, asyncNumber);
    }
}
