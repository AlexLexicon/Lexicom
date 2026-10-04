using IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;
using IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;
using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.Exceptions;
using Lexicom.Testing.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace IntegrationTests.For.Lexicom.Testing.DependencyInjection;

public class MakeTests
{
    [Fact]
    public async Task Successfully_Make_Type_With_Mixed_Dependencies()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.AddSingleton<IServiceDependencyIntReturnMethod, ServiceDependencyIntReturnMethod>();

        int intValueType = 123;
        var referenceTypeModel = new ReferenceTypeModel();
        var valueTypeModel = new ValueTypeModel
        {
            Id = Guid.NewGuid(),
        };

        //act
        var uot = ita.Make<ServiceWithMixedDependencies>(intValueType, referenceTypeModel, valueTypeModel);

        int mockableServiceNumber = uot._serviceDependencyIntReturnMethod.GetValueTypeIntMethod();
        int asyncMockableServiceNumber = await uot._serviceDependencyIntReturnMethod.GetValueTypeIntMethodAsync();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._serviceDependencyVoidReturnMethod);
        Assert.NotNull(uot._serviceDependencyIntReturnMethod);
        Assert.NotNull(uot._serviceDependencyStringReturnMethod);
        Assert.NotNull(uot._serviceDependencyReferenceTypeReturnMethod);

        Assert.False(uot.IsSubstitute());
        Assert.False(uot.ReferenceTypeModel.IsSubstitute());
        Assert.False(uot._serviceDependencyIntReturnMethod.IsSubstitute());

        Assert.True(uot._serviceDependencyVoidReturnMethod.IsSubstitute());
        Assert.True(uot._serviceDependencyStringReturnMethod.IsSubstitute());
        Assert.True(uot._serviceDependencyReferenceTypeReturnMethod.IsSubstitute());

        Assert.Equal(ServiceDependencyIntReturnMethod.NUMBER, mockableServiceNumber);
        Assert.Equal(ServiceDependencyIntReturnMethod.NUMBER, asyncMockableServiceNumber);
        Assert.Equal(intValueType, uot.IntValueType);
        Assert.Equal(referenceTypeModel, uot.ReferenceTypeModel);
        Assert.Equal(valueTypeModel, uot.ValueTypeModel);
    }

    [Fact]
    public void Fail_To_Make_When_Type_Is_Not_Registered()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        //assert
        Assert.Throws<MakeTypeIsNotRegisteredException>(() =>
        {
            //act
            var uot = ita.Make<IServiceDependencyVoidReturnMethod>();
        });
    }

    [Fact]
    public void Correctly_Make_Multiple_Dependencies_Such_That_A_Singleton_Is_Shared()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        string expectedString = string.Empty;
        int expectedNumber = 55;

        ita.Mock<IServiceDependencyStringReturnMethod>(MockLifetime.Transient).So(d =>
        {
            expectedString += "a";

            d.GetStringMethod().Returns(expectedString);
        });
        ita.Mock<IServiceDependencyIntReturnMethod>().So(d =>
        {
            d.GetValueTypeIntMethod().Returns(expectedNumber);
        });

        //act
        var uot1 = ita.Make<ServiceWithDependencies>();
        var uot2 = ita.Make<AnotherServiceWithDependencies>();

        (int number1, string str1) = uot1.GetValues();
        (int number2, string str2) = uot2.GetValues();

        //assert
        Assert.Equal(expectedNumber, number1);
        Assert.Equal("a", str1);

        Assert.Equal(expectedNumber, number2);
        Assert.Equal("aa", str2);
    }

    [Fact]
    public void Automatically_Inject_Empty_Enumerable_When_Generic_Argument_Is_Not_Mocked()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        //act
        var uot = ita.Make<ServiceWithEnumerableDependency>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._serviceDependencyIntReturnMethods);

        Assert.False(uot.IsSubstitute());

        Assert.Empty(uot._serviceDependencyIntReturnMethods);
    }

    [Fact]
    public void Inject_Empty_Enumerable_When_Not_Automatically_Mocking_And_Generic_Argument_Is_Not_Mocked()
    {
        //arrange
        var ita = new IntegrationTestAssistant(new TestAssistantConfiguration
        {
            IsAutomaticallyMocking = false,
        });

        //act
        var uot = ita.Make<ServiceWithEnumerableDependency>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._serviceDependencyIntReturnMethods);

        Assert.False(uot.IsSubstitute());

        Assert.Empty(uot._serviceDependencyIntReturnMethods);
    }

    [Fact]
    public void Inject_Empty_Enumerable_When_Not_Automatically_Mocking_And_Only_Generic_Argument_Is_Mocked()
    {
        //arrange
        var ita = new IntegrationTestAssistant(new TestAssistantConfiguration
        {
            IsAutomaticallyMocking = false,
        });

        ita.Mock<IServiceDependencyIntReturnMethod>();

        //act
        var uot = ita.Make<ServiceWithEnumerableDependency>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._serviceDependencyIntReturnMethods);

        Assert.False(uot.IsSubstitute());

        Assert.Empty(uot._serviceDependencyIntReturnMethods);
    }

    [Fact]
    public void Automatically_Inject_Mocked_Generic_Argument_As_Only_Enumerable_Item()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        int expectedNumber = 123;
        ita.Mock<IServiceDependencyIntReturnMethod>().So(d =>
        {
            d.GetValueTypeIntMethod().Returns(expectedNumber);
        });

        //act
        var uot = ita.Make<ServiceWithEnumerableDependency>();

        IServiceDependencyIntReturnMethod serviceDependencyIntReturnMethod = Assert.Single(uot._serviceDependencyIntReturnMethods);

        int number = serviceDependencyIntReturnMethod.GetValueTypeIntMethod();

        //assert
        Assert.NotNull(uot);

        Assert.False(uot.IsSubstitute());
        Assert.True(serviceDependencyIntReturnMethod.IsSubstitute());

        Assert.Equal(expectedNumber, number);
    }

    [Fact]
    public void Inject_Exact_Enumerable_Mock_Over_Mocked_Generic_Argument()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        IServiceDependencyIntReturnMethod[] serviceDependencyIntReturnMethods =
        [
            new ServiceDependencyIntReturnMethod(),
            new ServiceDependencyIntReturnMethod(),
        ];

        ita.Mock<IServiceDependencyIntReturnMethod>();
        ita.Mock<IEnumerable<IServiceDependencyIntReturnMethod>>().With(serviceDependencyIntReturnMethods);

        //act
        var uot = ita.Make<ServiceWithEnumerableDependency>();

        //assert
        Assert.NotNull(uot);

        Assert.False(uot.IsSubstitute());

        Assert.Same(serviceDependencyIntReturnMethods, uot._serviceDependencyIntReturnMethods);
    }

    [Fact]
    public void Inject_Registered_Generic_Arguments_As_Enumerable_Items()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.AddSingleton<IServiceDependencyIntReturnMethod, ServiceDependencyIntReturnMethod>();
        ita.AddSingleton<IServiceDependencyIntReturnMethod, ServiceDependencyIntReturnMethod>();

        //act
        var uot = ita.Make<ServiceWithEnumerableDependency>();

        //assert
        Assert.NotNull(uot);

        Assert.False(uot.IsSubstitute());

        Assert.Equal(2, uot._serviceDependencyIntReturnMethods.Count());
        Assert.All(uot._serviceDependencyIntReturnMethods, sdirm => Assert.False(sdirm.IsSubstitute()));
    }

    [Fact]
    public void Automatically_Inject_Substitute_Generic_Dependency_When_Open_Generic_Is_Not_Registered()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        //act
        var uot = ita.Make<ServiceWithGenericDependency>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._serviceDependencyGenericReturnMethod);

        Assert.False(uot.IsSubstitute());
        Assert.True(uot._serviceDependencyGenericReturnMethod.IsSubstitute());
    }

    [Fact]
    public void Inject_Mocked_Generic_Dependency()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        var expectedReferenceTypeModel = new ReferenceTypeModel();
        ita.Mock<IServiceDependencyGenericReturnMethod<ReferenceTypeModel>>().So(d =>
        {
            d.GetGenericMethod().Returns(expectedReferenceTypeModel);
        });

        //act
        var uot = ita.Make<ServiceWithGenericDependency>();

        ReferenceTypeModel? referenceTypeModel = uot._serviceDependencyGenericReturnMethod.GetGenericMethod();

        //assert
        Assert.NotNull(uot);

        Assert.False(uot.IsSubstitute());
        Assert.True(uot._serviceDependencyGenericReturnMethod.IsSubstitute());

        Assert.Same(expectedReferenceTypeModel, referenceTypeModel);
    }

    [Fact]
    public void Inject_Registered_Open_Generic_Dependency()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.AddSingleton(typeof(IServiceDependencyGenericReturnMethod<>), typeof(ServiceDependencyGenericReturnMethod<>));

        //act
        var uot = ita.Make<ServiceWithGenericDependency>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._serviceDependencyGenericReturnMethod);

        Assert.False(uot.IsSubstitute());
        Assert.False(uot._serviceDependencyGenericReturnMethod.IsSubstitute());
        Assert.IsType<ServiceDependencyGenericReturnMethod<ReferenceTypeModel>>(uot._serviceDependencyGenericReturnMethod);
    }

    [Fact]
    public void GetService_Does_Not_Automatically_Mock_Generic_Dependency()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        //act
        object? service = ita.GetService(typeof(IServiceDependencyGenericReturnMethod<ReferenceTypeModel>));

        //assert
        Assert.Null(service);
    }

    [Fact]
    public void GetService_Resolves_Mocked_Generic_Dependency()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        var serviceDependencyGenericReturnMethod = ita.Mock<IServiceDependencyGenericReturnMethod<ReferenceTypeModel>>(MockLifetime.Singleton).Pull();

        //act
        object? service = ita.GetService(typeof(IServiceDependencyGenericReturnMethod<ReferenceTypeModel>));

        //assert
        Assert.NotNull(service);

        Assert.Same(serviceDependencyGenericReturnMethod, service);
    }
}
