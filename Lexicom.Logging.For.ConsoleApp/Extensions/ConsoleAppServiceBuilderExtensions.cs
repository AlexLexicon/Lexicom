using Lexicom.Logging.Extensions;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.Logging.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddLogging(this IConsoleAppServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomLogging(builder.ConsoleApplicationBuilder.Configuration);

        return builder;
    }
}
