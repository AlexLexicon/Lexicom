namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithConcreteDependency
{
    public readonly AnotherServiceWithConcreteDependency _anotherServiceWithConcreteDependency;

    public ServiceWithConcreteDependency(AnotherServiceWithConcreteDependency anotherServiceWithConcreteDependency)
    {
        _anotherServiceWithConcreteDependency = anotherServiceWithConcreteDependency;
    }
}
