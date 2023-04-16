using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Lexicom.Supports.Blazor.WebAssembly;
public interface IBlazorWebAssemblyServiceBuilder
{
    WebAssemblyHostBuilder WebAssemblyHostBuilder { get; }
}
public class BlazorWebAssemblyServiceBuilder : IBlazorWebAssemblyServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public BlazorWebAssemblyServiceBuilder(WebAssemblyHostBuilder webAssemblyHostBuilder)
    {
        ArgumentNullException.ThrowIfNull(webAssemblyHostBuilder);

        WebAssemblyHostBuilder = webAssemblyHostBuilder;
    }

    public WebAssemblyHostBuilder WebAssemblyHostBuilder { get; }
}
