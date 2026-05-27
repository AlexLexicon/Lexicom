namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithInterfaceDependencies
{
    public readonly IServiceDependencyVoidReturnMethod _serviceDependencyVoidReturnMethod;
    public readonly IServiceDependencyIntReturnMethod _serviceDependencyIntReturnMethod;
    public readonly IServiceDependencyStringReturnMethod _serviceDependencyStringReturnMethod;
    public readonly IServiceDependencyReferenceTypeReturnMethod _serviceDependencyReferenceTypeReturnMethod;

    public ServiceWithInterfaceDependencies(
        IServiceDependencyVoidReturnMethod serviceDependencyVoidReturnMethod,
        IServiceDependencyIntReturnMethod serviceDependencyIntReturnMethod,
        IServiceDependencyStringReturnMethod serviceDependencyStringReturnMethod,
        IServiceDependencyReferenceTypeReturnMethod serviceDependencyReferenceTypeReturnMethod)
    {
        _serviceDependencyVoidReturnMethod = serviceDependencyVoidReturnMethod;
        _serviceDependencyIntReturnMethod = serviceDependencyIntReturnMethod;
        _serviceDependencyStringReturnMethod = serviceDependencyStringReturnMethod;
        _serviceDependencyReferenceTypeReturnMethod = serviceDependencyReferenceTypeReturnMethod;
    }
}
