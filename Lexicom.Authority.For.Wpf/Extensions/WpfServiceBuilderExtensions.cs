using Lexicom.Authority.Extensions;
using Lexicom.Supports.Wpf;

namespace Lexicom.Authority.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddAuthority(this IWpfServiceBuilder builder, Action<IAuthorityServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WpfApplicationBuilder.Services.AddLexicomAuthority(configure);

        return builder;
    }
}
