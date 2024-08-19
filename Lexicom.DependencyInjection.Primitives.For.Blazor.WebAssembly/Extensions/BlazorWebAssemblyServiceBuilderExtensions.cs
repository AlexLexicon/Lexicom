using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.Supports.Blazor.WebAssembly;

namespace Lexicom.DependencyInjection.Primitives.For.Blazor.WebAssembly.Extensions;
public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddPrimitives(this IBlazorWebAssemblyServiceBuilder builder, Action<IDependencyInjectionPrimitivesServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomDependencyInjectionPrimitives(configure);

        return builder;
    }
}
