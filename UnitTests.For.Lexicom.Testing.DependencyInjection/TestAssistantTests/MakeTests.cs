using Lexicom.Testing.DependencyInjection;
using Lexicom.Testing.DependencyInjection.Exceptions;
using Lexicom.Testing.DependencyInjection.Extensions;
using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;
using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.TestAssistantTests;

public class MakeTests
{
    [Fact]
    public void Automatically_Inject_Substitute_Dependencies()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        //act
        var uot = uta.Make<ServiceWithInterfaceDependencies>();

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
    public void Automatically_Inject_Substitute_ReferenceType_Model_Dependency()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        //act
        var uot = uta.Make<ServiceWithReferenceTypeModelDependency>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot.ReferenceTypeModel);

        Assert.False(uot.IsSubstitute());
        Assert.True(uot.ReferenceTypeModel.IsSubstitute());
    }

    [Fact]
    public void Manually_Inject_ReferenceType_Model_Dependency()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        var referenceTypeModel = new ReferenceTypeModel();

        //act
        var uot = uta.Make<ServiceWithReferenceTypeModelDependency>(referenceTypeModel);

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot.ReferenceTypeModel);

        Assert.False(uot.IsSubstitute());
        Assert.False(uot.ReferenceTypeModel.IsSubstitute());

        Assert.Equal(referenceTypeModel, uot.ReferenceTypeModel);
    }

    [Fact]
    public void Manually_Inject_ValueType_Dependency()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        int intValueType = 123;

        //act
        var uot = uta.Make<ServiceWithValueTypeDependency>(intValueType);

        //assert
        Assert.NotNull(uot);

        Assert.False(uot.IsSubstitute());

        Assert.Equal(intValueType, uot.IntValueType);
    }

    [Fact]
    public async Task Manually_Inject_Some_Dependencies()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        uta.Mock<IServiceDependencyIntReturnMethod>().With(new ServiceDependencyIntReturnMethod());

        int intValueType = 123;
        var referenceTypeModel = new ReferenceTypeModel();
        var valueTypeModel = new ValueTypeModel
        {
            Id = Guid.NewGuid(),
        };

        //act
        var uot = uta.Make<ServiceWithMixedDependencies>(intValueType, referenceTypeModel, valueTypeModel);

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
    public void Fail_To_Make_When_Unused_Manual_Dependency()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        var unusedManualParameter = new ReferenceTypeModel();

        //assert
        Assert.Throws<MakeUnusedManualParametersException>(() =>
        {
            //act
            var uot = uta.Make<ServiceWithInterfaceDependencies>(unusedManualParameter);
        });
    }

    [Fact]
    public void Successfully_Make_Type_With_No_Dependencies()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        //act
        var uot = uta.Make<ServiceWithNoDependencies>();

        //assert
        Assert.NotNull(uot);

        Assert.False(uot.IsSubstitute());
    }

    [Fact]
    public void Automatically_Inject_Substitute_To_Substitute_With_Constructor_Parameters()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        //act
        var uot = uta.Make<ServiceWithConcreteDependency>();

        //assert
        Assert.NotNull(uot);
        Assert.NotNull(uot._anotherServiceWithConcreteDependency);
        Assert.NotNull(uot._anotherServiceWithConcreteDependency._serviceWithNoDependencies);

        Assert.False(uot.IsSubstitute());
        Assert.True(uot._anotherServiceWithConcreteDependency.IsSubstitute());
        Assert.True(uot._anotherServiceWithConcreteDependency._serviceWithNoDependencies.IsSubstitute());
    }

    [Fact]
    public void Successfully_Make_With_Chained_Make_Calls()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        uta.Mock<ServiceChainB>().With(() =>
        {
            var serviceChainC = uta.Make<ServiceChainC>();

            return new ServiceChainB(serviceChainC);
        });

        //act
        var uot = uta.Make<ServiceChain>();

        string text = uot._serviceChainA.GetAText();

        //assert
        Assert.Equal("abc", text);
    }

    [Fact]
    public void Fail_To_Inject_ValueType_Dependency()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        //assert
        Assert.Throws<PullValueTypeException>(() =>
        {
            //act
            var uot = uta.Make<ServiceWithValueTypeDependency>();
        });
    }

    [Fact]
    public void Fail_To_Inject_ValueType_Dependencies()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        //assert
        Assert.Throws<PullValueTypeException>(() =>
        {
            //act
            var uot = uta.Make<ServiceWithValueTypeDependencies>();
        });
    }

    [Fact]
    public void Fail_To_Inject_Nullable_ValueType_Dependency()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        //assert
        Assert.Throws<PullValueTypeException>(() =>
        {
            //act
            var uot = uta.Make<ServiceWithNullableValueTypeDependency>();
        });
    }

    [Fact]
    public void Fail_To_Inject_ValueType_Model_Dependency()
    {
        //arrange
        using var uta = new UnitTestAssistant();

        //assert
        Assert.Throws<PullValueTypeException>(() =>
        {
            //act
            var uot = uta.Make<ServiceWithValueTypeModelDependency>();
        });
    }
}
