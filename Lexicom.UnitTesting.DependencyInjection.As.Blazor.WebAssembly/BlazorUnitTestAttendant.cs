namespace Lexicom.UnitTesting.DependencyInjection.As.Blazor.WebAssembly;

public class BlazorUnitTestAttendant : UnitTestAttendant
{
    public BlazorUnitTestAttendant()
    {
        BlazorBuilder = new BlazorWebAssemblyServiceBuilder(this);
    }

    public BlazorWebAssemblyServiceBuilder BlazorBuilder { get; }
}
