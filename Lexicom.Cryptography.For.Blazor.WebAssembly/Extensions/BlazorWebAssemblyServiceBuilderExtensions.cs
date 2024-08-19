using Lexicom.Cryptography.Extensions;
using Lexicom.Supports.Blazor.WebAssembly;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Cryptography.For.Blazor.WebAssembly.Extensions;
public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddCryptography(this IBlazorWebAssemblyServiceBuilder builder, Action<ICryptographyServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomCryptography(configure);

        builder.Services.Replace(new ServiceDescriptor(typeof(IAesProvider), typeof(BlazorAesProvider), ServiceLifetime.Singleton));

        return builder;
    }
}