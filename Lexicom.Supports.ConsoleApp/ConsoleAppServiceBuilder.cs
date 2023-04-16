using Lexicom.ConsoleApp.DependencyInjection;

namespace Lexicom.Supports.ConsoleApp;
public interface IConsoleAppServiceBuilder
{
    ConsoleApplicationBuilder ConsoleApplicationBuilder { get; }
}
public class ConsoleAppServiceBuilder : IConsoleAppServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public ConsoleAppServiceBuilder(ConsoleApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        ConsoleApplicationBuilder = builder;
    }

    public ConsoleApplicationBuilder ConsoleApplicationBuilder { get; }
}
