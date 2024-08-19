using Lexicom.Configuration.AzureKeyVault.Extensions;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.Configuration.AzureKeyVault.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddValidation(this IConsoleAppServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Configuration.AddLexicomAzureKeyVault();

        return builder;
    }
}
