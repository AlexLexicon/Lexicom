using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Lexicom.DependencyInjection.Hosting;

namespace Lexicom.Supports.Blazor.WebAssembly.Extensions;
public static class WebAssemblyHostBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WebAssemblyHostBuilder Lexicom(this WebAssemblyHostBuilder builder, Action<IBlazorWebAssemblyServiceBuilder>? configure, bool useLexicomContainerForHosting = true)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new BlazorWebAssemblyServiceBuilder(builder));

        if (useLexicomContainerForHosting)
        {
            builder.AddLexicomContainerForHosting();
        }

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static WebAssemblyHostBuilder AddLexicomContainerForHosting(this WebAssemblyHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureContainer(new LexicomServiceProviderFactory());

        return builder;
    }
}
