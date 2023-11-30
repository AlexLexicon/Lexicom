using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Lexicom.DependencyInjection.Hosting;

namespace Lexicom.Supports.Blazor.WebAssembly.Extensions;
public static class WebAssemblyHostBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WebAssemblyHostBuilder Lexicom(this WebAssemblyHostBuilder builder, Action<IBlazorWebAssemblyServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new BlazorWebAssemblyServiceBuilder(builder));

        builder.AddLexicomHosting();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static WebAssemblyHostBuilder AddLexicomHosting(this WebAssemblyHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureContainer(new LexicomServiceProviderFactory());

        return builder;
    }
}
