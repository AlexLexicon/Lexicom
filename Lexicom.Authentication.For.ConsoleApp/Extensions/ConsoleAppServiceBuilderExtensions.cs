using Lexicom.Authentication.Extensions;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.Authentication.For.ConsoleApp.Extensions;

public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddAuthentication(this IConsoleAppServiceBuilder builder, Action<IAuthenticationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomAuthentication(builder.Configuration, configure);

        return builder;
    }
}
