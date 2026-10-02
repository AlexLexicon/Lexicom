namespace UnitTests.For.Lexicom.Testing.DependencyInjection.Constructs.Services;

public interface IServiceDependencyStringReturnMethod
{
    string GetStringMethod();
    Task<string> GetStringAsync();
}
