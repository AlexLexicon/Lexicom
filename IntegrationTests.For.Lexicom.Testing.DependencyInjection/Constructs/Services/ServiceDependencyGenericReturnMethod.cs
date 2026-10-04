namespace IntegrationTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public interface IServiceDependencyGenericReturnMethod<T>
{
    T? GetGenericMethod();
}
public class ServiceDependencyGenericReturnMethod<T> : IServiceDependencyGenericReturnMethod<T>
{
    public T? GetGenericMethod()
    {
        return default;
    }
}
