using Lexicom.Supports.ConsoleApp;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddValidation(this IConsoleAppServiceBuilder builder, Action<IValidationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomValidation(configure);

        return builder;
    }
}
