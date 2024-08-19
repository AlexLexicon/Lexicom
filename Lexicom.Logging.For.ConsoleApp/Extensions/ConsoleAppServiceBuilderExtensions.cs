using Lexicom.Logging.Extensions;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.Logging.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddLogging(this IConsoleAppServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomLogging(builder.Configuration);

        return builder;
    }
}
