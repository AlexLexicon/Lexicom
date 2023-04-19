using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Lexicom.Supports.Blazor.WebAssembly.Extensions;
public static class WebAssemblyHostBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WebAssemblyHostBuilder Lexicom(this WebAssemblyHostBuilder builder, Action<IBlazorWebAssemblyServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureContainer(new LexicomBlazorWebAssemblyServiceProviderFactory());

        configure?.Invoke(new BlazorWebAssemblyServiceBuilder(builder));

        return builder;
    }
}
