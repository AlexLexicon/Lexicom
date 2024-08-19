using Lexicom.Cryptography.Extensions;
using Lexicom.Supports.Blazor.WebAssembly;

namespace Lexicom.Cryptography.For.Blazor.WebAssembly.Extensions;
public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddCryptography(this IBlazorWebAssemblyServiceBuilder builder, Action<ICryptographyServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebAssemblyHostBuilder.Services.AddLexicomCryptography(configure);

        return builder;
    }
}