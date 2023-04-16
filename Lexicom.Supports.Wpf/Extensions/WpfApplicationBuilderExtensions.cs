using Lexicom.Wpf.DependencyInjection;

namespace Lexicom.Supports.Wpf.Extensions;
public static class WpfApplicationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WpfApplicationBuilder Lexicom(this WpfApplicationBuilder builder, Action<IWpfServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new WpfServiceBuilder(builder));

        return builder;
    }
}