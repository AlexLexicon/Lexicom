using Lexicom.Testing.DependencyInjection;
using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;
using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.TestAssistantConfigurationTests;

public class DefaultMockLifetimeTests
{
    [Fact]
    public void Singleton_DefaultMockLifetime_Makes_Unspecified_Mocks_Singleton()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            DefaultMockLifetime = MockLifetime.Singleton,
        });

        int factoryCallCount = 0;
        uta.Mock<ReferenceTypeModel>().With(() =>
        {
            factoryCallCount++;

            return new ReferenceTypeModel
            {
                Value = "test",
            };
        });

        //act
        var uot1 = uta.Make<ServiceWithReferenceTypeModelDependency>();
        var uot2 = uta.Make<ServiceWithReferenceTypeModelDependency>();

        Guid uot1ModelId = uot1.ReferenceTypeModel.Id;
        Guid uot2ModelId = uot2.ReferenceTypeModel.Id;

        //assert
        Assert.Equal(1, factoryCallCount);
        Assert.Equal(uot1ModelId, uot2ModelId);
    }

    [Fact]
    public void Transient_DefaultMockLifetime_Makes_Unspecified_Mocks_Transient()
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            DefaultMockLifetime = MockLifetime.Transient,
        });

        int factoryCallCount = 0;
        uta.Mock<ReferenceTypeModel>().With(() =>
        {
            factoryCallCount++;

            return new ReferenceTypeModel
            {
                Value = "test",
            };
        });

        //act
        var uot1 = uta.Make<ServiceWithReferenceTypeModelDependency>();
        var uot2 = uta.Make<ServiceWithReferenceTypeModelDependency>();

        Guid uot1ModelId = uot1.ReferenceTypeModel.Id;
        Guid uot2ModelId = uot2.ReferenceTypeModel.Id;

        //assert
        Assert.Equal(2, factoryCallCount);
        Assert.NotEqual(uot1ModelId, uot2ModelId);
    }

    [Theory]
    [InlineData(MockLifetime.Singleton, MockLifetime.Transient, false, 2)]
    [InlineData(MockLifetime.Transient, MockLifetime.Singleton, true, 1)]
    public void Specified_Mocks_Override_DefaultMockLifetime_Configuration(MockLifetime defaultMockLifetime, MockLifetime actualMockLifetime, bool isEqual, int expectedFactoryCallCount)
    {
        //arrange
        using var uta = new UnitTestAssistant(new TestAssistantConfiguration
        {
            DefaultMockLifetime = defaultMockLifetime,
        });

        int factoryCallCount = 0;
        uta.Mock<ReferenceTypeModel>(actualMockLifetime).With(() =>
        {
            factoryCallCount++;

            return new ReferenceTypeModel
            {
                Value = "test",
            };
        });

        //act
        var uot1 = uta.Make<ServiceWithReferenceTypeModelDependency>();
        var uot2 = uta.Make<ServiceWithReferenceTypeModelDependency>();

        Guid uot1ModelId = uot1.ReferenceTypeModel.Id;
        Guid uot2ModelId = uot2.ReferenceTypeModel.Id;

        //assert
        Assert.Equal(expectedFactoryCallCount, factoryCallCount);
        if (isEqual)
        {
            Assert.Equal(uot1ModelId, uot2ModelId);
        }
        else
        {
            Assert.NotEqual(uot1ModelId, uot2ModelId);
        }
    }
}
