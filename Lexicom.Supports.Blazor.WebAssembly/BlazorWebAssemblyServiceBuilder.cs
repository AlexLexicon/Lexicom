using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Supports.Blazor.WebAssembly;

public interface IBlazorWebAssemblyServiceBuilder
{
    IServiceCollection Services { get; }
}
public interface IDependentBlazorWebAssemblyServiceBuilder : IBlazorWebAssemblyServiceBuilder
{
    WebAssemblyHostBuilder WebAssemblyHostBuilder { get; }
}
public class BlazorWebAssemblyServiceBuilder : IDependentBlazorWebAssemblyServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public BlazorWebAssemblyServiceBuilder(WebAssemblyHostBuilder webAssemblyHostBuilder)
    {
        ArgumentNullException.ThrowIfNull(webAssemblyHostBuilder);

        WebAssemblyHostBuilder = webAssemblyHostBuilder;
    }

    public WebAssemblyHostBuilder WebAssemblyHostBuilder { get; }
    public IServiceCollection Services => WebAssemblyHostBuilder.Services;
}
