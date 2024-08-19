namespace Lexicom.UnitTesting.DependencyInjection.As.Wpf;
public class WpfUnitTestAttendant : UnitTestAttendant
{
    public WpfUnitTestAttendant()
    {
        WpfBuilder = new TestWpfServiceBuilder(this);
    }

    public TestWpfServiceBuilder WpfBuilder { get; }
}
