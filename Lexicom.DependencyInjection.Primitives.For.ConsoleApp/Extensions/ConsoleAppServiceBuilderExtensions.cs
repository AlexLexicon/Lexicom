using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.DependencyInjection.Primitives.For.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddPrimitives(this IConsoleAppServiceBuilder builder, Action<IDependencyInjectionPrimitivesServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomDependencyInjectionPrimitives(configure);

        return builder;
    }
}
