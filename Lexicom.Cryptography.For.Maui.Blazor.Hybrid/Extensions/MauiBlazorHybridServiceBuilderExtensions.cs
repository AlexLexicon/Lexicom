using Lexicom.Cryptography.Extensions;
using Lexicom.Supports.Maui.Blazor.Hybrid;

namespace Lexicom.Cryptography.For.Maui.Blazor.Hybrid.Extensions;
public static class MauiBlazorHybridServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IMauiBlazorHybridServiceBuilder AddCryptography(this IMauiBlazorHybridServiceBuilder builder, Action<ICryptographyServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomCryptography(configure);

        return builder;
    }
}
