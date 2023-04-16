using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.Supports.Blazor.WebAssembly;

namespace Lexicom.DependencyInjection.Primitives.For.Blazor.WebAssembly.Extensions;
public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddTimeProvider(this IBlazorWebAssemblyServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebAssemblyHostBuilder.Services.AddLexicomTimeProvider();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddGuidProvider(this IBlazorWebAssemblyServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebAssemblyHostBuilder.Services.AddLexicomGuidProvider();

        return builder;
    }
}
