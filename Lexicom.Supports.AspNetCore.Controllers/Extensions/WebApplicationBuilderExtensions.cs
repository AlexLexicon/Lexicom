using Lexicom.DependencyInjection.Hosting;
using Microsoft.AspNetCore.Builder;

namespace Lexicom.Supports.AspNetCore.Controllers.Extensions;
public static class WebApplicationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WebApplicationBuilder Lexicom(this WebApplicationBuilder builder, Action<IDependantAspNetCoreControllersServiceBuilder>? configure, bool configureContainerForLexicomHostingFeatures = true)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new AspNetCoreControllersServiceBuilder(builder));

        if (configureContainerForLexicomHostingFeatures)
        {
            builder.ConfigureContainerForLexicomHostingFeatures();
        }

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static WebApplicationBuilder ConfigureContainerForLexicomHostingFeatures(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Host.UseServiceProviderFactory(new LexicomServiceProviderFactory());

        return builder;
    }
}