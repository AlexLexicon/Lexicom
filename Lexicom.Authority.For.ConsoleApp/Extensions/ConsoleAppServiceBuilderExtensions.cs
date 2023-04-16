using Lexicom.Authority.Extensions;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.Authority.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddAuthority(this IConsoleAppServiceBuilder builder, Action<IAuthorityServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomAuthority(configure);

        return builder;
    }
}
