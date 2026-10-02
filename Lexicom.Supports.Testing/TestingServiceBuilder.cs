using Lexicom.Testing.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Supports.Testing;

public interface ITestingServiceBuilder
{
    IServiceCollection Services { get; }
    ConfigurationManager Configuration { get; }
}
public class TestingServiceBuilder : ITestingServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public TestingServiceBuilder(IntegrationTestAssistant assistant)
    {
        ArgumentNullException.ThrowIfNull(assistant);

        Services = assistant;
        Configuration = assistant.Configuration;
    }

    public IServiceCollection Services { get; }
    public ConfigurationManager Configuration { get; }
}
