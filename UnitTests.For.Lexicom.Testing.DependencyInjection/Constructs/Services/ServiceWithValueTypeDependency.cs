namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithValueTypeDependency
{
    public ServiceWithValueTypeDependency(int intValueType)
    {
        IntValueType = intValueType;
    }

    public int IntValueType { get; }
}
