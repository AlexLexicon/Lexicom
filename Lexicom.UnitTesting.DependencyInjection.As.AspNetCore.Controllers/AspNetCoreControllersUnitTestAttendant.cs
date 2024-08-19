namespace Lexicom.UnitTesting.DependencyInjection.As.AspNetCore.Controllers;
public class AspNetCoreControllersUnitTestAttendant : UnitTestAttendant
{
    public AspNetCoreControllersUnitTestAttendant()
    {
        AspNetCoreBuilder = new TestAspNetCoreControllersServiceBuilder(this);
    }

    public TestAspNetCoreControllersServiceBuilder AspNetCoreBuilder { get; }
}
