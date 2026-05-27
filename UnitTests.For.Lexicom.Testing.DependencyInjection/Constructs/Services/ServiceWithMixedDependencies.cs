using UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;

namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithMixedDependencies
{
    public readonly IServiceDependencyVoidReturnMethod _serviceDependencyVoidReturnMethod;
    public readonly IServiceDependencyIntReturnMethod _serviceDependencyIntReturnMethod;
    public readonly IServiceDependencyStringReturnMethod _serviceDependencyStringReturnMethod;
    public readonly IServiceDependencyReferenceTypeReturnMethod _serviceDependencyReferenceTypeReturnMethod;

    public ServiceWithMixedDependencies(
        IServiceDependencyVoidReturnMethod serviceDependencyVoidReturnMethod,
        int intValueType,
        IServiceDependencyIntReturnMethod serviceDependencyIntReturnMethod,
        ReferenceTypeModel referenceTypeModel,
        IServiceDependencyStringReturnMethod serviceDependencyStringReturnMethod,
        ValueTypeModel valueTypeModel,
        IServiceDependencyReferenceTypeReturnMethod serviceDependencyReferenceTypeReturnMethod)
    {
        _serviceDependencyVoidReturnMethod = serviceDependencyVoidReturnMethod;
        _serviceDependencyIntReturnMethod = serviceDependencyIntReturnMethod;
        _serviceDependencyStringReturnMethod = serviceDependencyStringReturnMethod;
        _serviceDependencyReferenceTypeReturnMethod = serviceDependencyReferenceTypeReturnMethod;

        IntValueType = intValueType;
        ReferenceTypeModel = referenceTypeModel;
        ValueTypeModel = valueTypeModel;
    }

    public int IntValueType { get; }
    public ReferenceTypeModel ReferenceTypeModel { get; }
    public ValueTypeModel ValueTypeModel { get; }
}
