namespace Lexicom.UnitTesting.DependencyInjection.As.ConsoleApp;
public class ConsoleAppUnitTestAttendant : UnitTestAttendant
{
    public ConsoleAppUnitTestAttendant()
    {
        ConsoleAppBuilder = new TestConsoleAppServiceBuilder(this);
    }

    public TestConsoleAppServiceBuilder ConsoleAppBuilder { get; }
}
