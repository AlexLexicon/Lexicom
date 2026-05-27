namespace IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithDependencies
{
    public readonly IServiceDependencyIntReturnMethod _serviceDependencyIntReturnMethod;
    public readonly IServiceDependencyStringReturnMethod _serviceDependencyStringReturnMethod;

    public ServiceWithDependencies(
        IServiceDependencyIntReturnMethod serviceDependencyIntReturnMethod,
        IServiceDependencyStringReturnMethod serviceDependencyStringReturnMethod)
    {
        _serviceDependencyIntReturnMethod = serviceDependencyIntReturnMethod;
        _serviceDependencyStringReturnMethod = serviceDependencyStringReturnMethod;
    }

    public (int number, string str) GetValues()
    {
        int number = _serviceDependencyIntReturnMethod.GetValueTypeIntMethod();
        string str = _serviceDependencyStringReturnMethod.GetStringMethod();

        return (number, str);
    }
}
