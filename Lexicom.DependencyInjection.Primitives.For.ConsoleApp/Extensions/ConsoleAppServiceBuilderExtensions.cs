using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.DependencyInjection.Primitives.For.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddTimeProvider(this IConsoleAppServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomTimeProvider();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddGuidProvider(this IConsoleAppServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomGuidProvider();

        return builder;
    }
}
