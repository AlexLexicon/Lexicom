using Lexicom.Configuration.AzureKeyVault.Extensions;
using Lexicom.Supports.Wpf;

namespace Lexicom.Configuration.AzureKeyVault.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddValidation(this IWpfServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WpfApplicationBuilder.Configuration.AddLexicomAzureKeyVault();

        return builder;
    }
}
