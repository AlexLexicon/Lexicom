namespace IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithEnumerableDependency
{
    public readonly IEnumerable<IServiceDependencyIntReturnMethod> _serviceDependencyIntReturnMethods;

    public ServiceWithEnumerableDependency(IEnumerable<IServiceDependencyIntReturnMethod> serviceDependencyIntReturnMethods)
    {
        _serviceDependencyIntReturnMethods = serviceDependencyIntReturnMethods;
    }
}
