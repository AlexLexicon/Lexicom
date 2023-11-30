using Microsoft.AspNetCore.Builder;

namespace Lexicom.Supports.AspNetCore.Controllers.Extensions;
public static class WebApplicationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WebApplicationBuilder Lexicom(this WebApplicationBuilder builder, Action<IAspNetCoreControllersServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new AspNetCoreControllersServiceBuilder(builder));

        builder.AddLexicomHosting();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static WebApplicationBuilder AddLexicomHosting(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        //todo
        //aspnet web application builder does not seem to support a way to replace the service provider factory
        //:( will have to come back here in the future to try and force it with reflection or something
        //builder.ConfigureContainer(new LexicomServiceProviderFactory());

        return builder;
    }
}