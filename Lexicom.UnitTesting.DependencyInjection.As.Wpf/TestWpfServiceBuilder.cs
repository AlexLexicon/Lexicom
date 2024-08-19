using Lexicom.Supports.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.UnitTesting.DependencyInjection.As.Wpf;
public class TestWpfServiceBuilder : IWpfServiceBuilder
{
    private readonly WpfUnitTestAttendant _blazorUnitTestAttendant;

    public TestWpfServiceBuilder(WpfUnitTestAttendant blazorUnitTestAttendant)
    {
        _blazorUnitTestAttendant = blazorUnitTestAttendant;
    }

    public IServiceCollection Services => _blazorUnitTestAttendant;
    public ConfigurationManager Configuration => _blazorUnitTestAttendant.Configuration;
}
