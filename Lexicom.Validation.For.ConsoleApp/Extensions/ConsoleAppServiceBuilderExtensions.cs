using Lexicom.Supports.ConsoleApp;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddValidation(this IConsoleAppServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddValidation(builder, null);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddValidation(this IConsoleAppServiceBuilder builder, Action<IValidationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomValidation(builder.ConsoleApplicationBuilder.Configuration, configure);

        return builder;
    }
}
