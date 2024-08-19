using Lexicom.Supports.Blazor.WebAssembly;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.UnitTesting.DependencyInjection.As.Blazor.WebAssembly;
public class TestBlazorWebAssemblyServiceBuilder : IBlazorWebAssemblyServiceBuilder
{
    private readonly BlazorUnitTestAttendant _blazorUnitTestAttendant;

    public TestBlazorWebAssemblyServiceBuilder(BlazorUnitTestAttendant blazorUnitTestAttendant)
    {
        _blazorUnitTestAttendant = blazorUnitTestAttendant;
    }

    public IServiceCollection Services => _blazorUnitTestAttendant;
}
