using Lexicom.ConsoleApp.DependencyInjection;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.AspNetCore.Controllers.Extensions;
public static class ConsoleApplicationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ConsoleApplicationBuilder Lexicom(this ConsoleApplicationBuilder builder, Action<IConsoleAppServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new ConsoleAppServiceBuilder(builder));

        return builder;
    }
}