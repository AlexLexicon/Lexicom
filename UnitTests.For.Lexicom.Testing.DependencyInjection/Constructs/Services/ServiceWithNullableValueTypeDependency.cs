namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public class ServiceWithNullableValueTypeDependency
{
    public ServiceWithNullableValueTypeDependency(int? nullableIntType)
    {
        NullableIntType = nullableIntType;
    }

    public int? NullableIntType { get; }
}
