using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.Supports.Maui.Blazor.Hybrid;

namespace Lexicom.DependencyInjection.Primitives.For.Maui.Blazor.Hybrid.Extensions;
public static class MauiBlazorHybridServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IMauiBlazorHybridServiceBuilder AddPrimitives(this IMauiBlazorHybridServiceBuilder builder, Action<IDependencyInjectionPrimitivesServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomDependencyInjectionPrimitives(configure);

        return builder;
    }
}
