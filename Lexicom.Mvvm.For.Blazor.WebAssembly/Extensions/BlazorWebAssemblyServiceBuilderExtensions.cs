using Lexicom.Mvvm.Extensions;
using Lexicom.Supports.Blazor.WebAssembly;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly.Extensions;
public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IDependantBlazorWebAssemblyServiceBuilder AddMvvm(this IDependantBlazorWebAssemblyServiceBuilder builder, Action<IMvvmServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebAssemblyHostBuilder.Services.AddLexicomMvvm(configure);

        return builder;
    }
}
