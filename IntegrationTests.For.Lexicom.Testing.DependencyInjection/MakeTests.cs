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
}
