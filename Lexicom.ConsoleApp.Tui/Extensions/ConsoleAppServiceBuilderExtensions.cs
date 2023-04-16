using Lexicom.Supports.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.ConsoleApp.Tui.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddTui<TAssemblyScanMarker>(this IConsoleAppServiceBuilder builder, ServiceLifetime operationsLifetime = ServiceLifetime.Transient)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomConsoleAppTui<TAssemblyScanMarker>(operationsLifetime);

        return builder;
    }
}
