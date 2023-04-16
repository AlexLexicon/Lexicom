using Lexicom.Cryptography.Extensions;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.Cryptography.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddCryptography(this IConsoleAppServiceBuilder builder, Action<ICryptographyServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomCryptography(configure);

        return builder;
    }
}