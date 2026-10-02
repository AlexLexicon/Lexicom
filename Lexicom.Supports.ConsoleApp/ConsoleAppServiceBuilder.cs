using Lexicom.ConsoleApp.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Supports.ConsoleApp;

public interface IConsoleAppServiceBuilder
{
    IServiceCollection Services { get; }
    ConfigurationManager Configuration { get; }
}
public interface IDependentConsoleAppServiceBuilder : IConsoleAppServiceBuilder
{
    ConsoleApplicationBuilder ConsoleApplicationBuilder { get; }
}
public class ConsoleAppServiceBuilder : IDependentConsoleAppServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public ConsoleAppServiceBuilder(ConsoleApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        ConsoleApplicationBuilder = builder;
    }

    public ConsoleApplicationBuilder ConsoleApplicationBuilder { get; }
    public IServiceCollection Services => ConsoleApplicationBuilder.Services;
    public ConfigurationManager Configuration => ConsoleApplicationBuilder.Configuration;
}
