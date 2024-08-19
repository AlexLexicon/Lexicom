using Lexicom.Supports.ConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.UnitTesting.DependencyInjection.As.ConsoleApp;
public class TestConsoleAppServiceBuilder : IConsoleAppServiceBuilder
{
    private readonly ConsoleAppUnitTestAttendant _consoleAppUnitTestAttendant;

    public TestConsoleAppServiceBuilder(ConsoleAppUnitTestAttendant consoleAppUnitTestAttendant)
    {
        _consoleAppUnitTestAttendant = consoleAppUnitTestAttendant;
    }

    public IServiceCollection Services => _consoleAppUnitTestAttendant;
    public ConfigurationManager Configuration => _consoleAppUnitTestAttendant.Configuration;
}
