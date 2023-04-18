using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting;
public interface ITestDependencyInjectionPrimitivesServiceBuilder
{
    IServiceCollection Services { get; }
}
public class TestDependencyInjectionPrimitivesServiceBuilder : ITestDependencyInjectionPrimitivesServiceBuilder
{    
    /// <exception cref="ArgumentNullException"/>
    public TestDependencyInjectionPrimitivesServiceBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
    }

    public IServiceCollection Services { get; }
}
