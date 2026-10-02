namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class AnotherServiceWithConcreteDependency
{
    public readonly ServiceWithNoDependencies _serviceWithNoDependencies;

    public AnotherServiceWithConcreteDependency(ServiceWithNoDependencies serviceWithNoDependencies)
    {
        _serviceWithNoDependencies = serviceWithNoDependencies;
    }
}
