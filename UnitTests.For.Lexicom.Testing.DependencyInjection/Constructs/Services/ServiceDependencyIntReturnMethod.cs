namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public interface IServiceDependencyIntReturnMethod
{
    int GetValueTypeIntMethod();
    Task<int> GetValueTypeIntMethodAsync();
}
public class ServiceDependencyIntReturnMethod : IServiceDependencyIntReturnMethod
{
    public const int NUMBER = 12345;

    public int GetValueTypeIntMethod()
    {
        return NUMBER;
    }

    public Task<int> GetValueTypeIntMethodAsync()
    {
        return Task.FromResult(NUMBER);
    }
}
