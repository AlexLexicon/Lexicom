using Lexicom.ConsoleApp.DependencyInjection;
using Lexicom.DependencyInjection.Hosting;

namespace Lexicom.Supports.ConsoleApp.Extensions;

public static class ConsoleApplicationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ConsoleApplicationBuilder Lexicom(this ConsoleApplicationBuilder builder, Action<IDependentConsoleAppServiceBuilder>? configure, bool configureContainerForLexicomHostingFeatures = true)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new ConsoleAppServiceBuilder(builder));

        if (configureContainerForLexicomHostingFeatures)
        {
            builder.ConfigureContainerForLexicomHostingFeatures();
        }

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static ConsoleApplicationBuilder ConfigureContainerForLexicomHostingFeatures(this ConsoleApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureContainer(new LexicomServiceProviderFactory());

        return builder;
    }
}