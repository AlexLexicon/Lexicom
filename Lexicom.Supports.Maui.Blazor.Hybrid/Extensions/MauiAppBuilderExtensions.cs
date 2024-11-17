using Lexicom.DependencyInjection.Hosting;

namespace Lexicom.Supports.Maui.Blazor.Hybrid.Extensions;
public static class MauiAppBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static MauiAppBuilder Lexicom(this MauiAppBuilder builder, Action<IDependantMauiBlazorHybridServiceBuilder>? configure, bool configureContainerForLexicomHostingFeatures = true)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new MauiBlazorHybridServiceBuilder(builder));

        if (configureContainerForLexicomHostingFeatures)
        {
            builder.ConfigureContainerForLexicomHostingFeatures();
        }

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static MauiAppBuilder ConfigureContainerForLexicomHostingFeatures(this MauiAppBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureContainer(new LexicomServiceProviderFactory());

        return builder;
    }
}
