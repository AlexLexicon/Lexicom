using IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Models;

namespace IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithGenericDependency
{
    public readonly IServiceDependencyGenericReturnMethod<ReferenceTypeModel> _serviceDependencyGenericReturnMethod;

    public ServiceWithGenericDependency(IServiceDependencyGenericReturnMethod<ReferenceTypeModel> serviceDependencyGenericReturnMethod)
    {
        _serviceDependencyGenericReturnMethod = serviceDependencyGenericReturnMethod;
    }
}
