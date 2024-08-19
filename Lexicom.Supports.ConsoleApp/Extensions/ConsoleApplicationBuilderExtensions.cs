using Lexicom.ConsoleApp.DependencyInjection;
using Lexicom.DependencyInjection.Hosting;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.AspNetCore.Controllers.Extensions;
public static class ConsoleApplicationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ConsoleApplicationBuilder Lexicom(this ConsoleApplicationBuilder builder, Action<IDependantConsoleAppServiceBuilder>? configure, bool configureContainerForLexicomHostingFeatures = true)
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