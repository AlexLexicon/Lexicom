using Lexicom.Supports.AspNetCore.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.UnitTesting.DependencyInjection.As.AspNetCore.Controllers;
public class TestAspNetCoreControllersServiceBuilder : IAspNetCoreControllersServiceBuilder
{
    private readonly AspNetCoreControllersUnitTestAttendant _aspNetCoreControllersUnitTestAttendant;

    public TestAspNetCoreControllersServiceBuilder(AspNetCoreControllersUnitTestAttendant aspNetCoreControllersUnitTestAttendant)
    {
        _aspNetCoreControllersUnitTestAttendant = aspNetCoreControllersUnitTestAttendant;
    }

    public IServiceCollection Services => _aspNetCoreControllersUnitTestAttendant;
    public ConfigurationManager Configuration => _aspNetCoreControllersUnitTestAttendant.Configuration;
}
