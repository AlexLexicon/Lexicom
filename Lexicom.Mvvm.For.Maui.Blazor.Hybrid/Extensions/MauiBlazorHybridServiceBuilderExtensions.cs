using Lexicom.Mvvm.Extensions;
using Lexicom.Supports.Maui.Blazor.Hybrid;

namespace Lexicom.Mvvm.For.Maui.Blazor.Hybrid.Extensions;
public static class MauiBlazorHybridServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IMauiBlazorHybridServiceBuilder AddMvvm(this IMauiBlazorHybridServiceBuilder builder, Action<IMvvmServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomMvvm(configure);

        return builder;
    }
}
