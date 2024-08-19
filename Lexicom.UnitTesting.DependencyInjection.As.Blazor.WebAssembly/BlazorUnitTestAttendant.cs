namespace Lexicom.UnitTesting.DependencyInjection.As.Blazor.WebAssembly;
public class BlazorUnitTestAttendant : UnitTestAttendant
{
    public BlazorUnitTestAttendant()
    {
        BlazorBuilder = new TestBlazorWebAssemblyServiceBuilder(this);
    }

    public TestBlazorWebAssemblyServiceBuilder BlazorBuilder { get; }
}
