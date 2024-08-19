using Lexicom.Cryptography.Extensions;
using Lexicom.Supports.Wpf;

namespace Lexicom.Cryptography.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddCryptography(this IWpfServiceBuilder builder, Action<ICryptographyServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WpfApplicationBuilder.Services.AddLexicomCryptography(configure);

        return builder;
    }
}
