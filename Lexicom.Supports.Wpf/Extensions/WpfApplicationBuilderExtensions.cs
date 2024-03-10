using Lexicom.DependencyInjection.Hosting;
using Lexicom.Wpf.DependencyInjection;

namespace Lexicom.Supports.Wpf.Extensions;
public static class WpfApplicationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WpfApplicationBuilder Lexicom(this WpfApplicationBuilder builder, Action<IWpfServiceBuilder>? configure, bool configureContainerForLexicomHostingFeatures = true)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new WpfServiceBuilder(builder));

        if (configureContainerForLexicomHostingFeatures)
        {
            builder.ConfigureContainerForLexicomHostingFeatures();
        }

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static WpfApplicationBuilder ConfigureContainerForLexicomHostingFeatures(this WpfApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureContainer(new LexicomServiceProviderFactory());

        return builder;
    }
}