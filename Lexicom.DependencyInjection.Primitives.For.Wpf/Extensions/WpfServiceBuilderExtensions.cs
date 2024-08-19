using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.Supports.Wpf;

namespace Lexicom.DependencyInjection.Primitives.For.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddPrimitives(this IWpfServiceBuilder builder, Action<IDependencyInjectionPrimitivesServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomDependencyInjectionPrimitives(configure);

        return builder;
    }
}
