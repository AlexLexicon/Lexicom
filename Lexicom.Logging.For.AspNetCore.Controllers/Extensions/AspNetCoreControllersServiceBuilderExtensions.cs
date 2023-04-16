using Lexicom.Supports.AspNetCore.Controllers;
using Serilog;

namespace Lexicom.Smtp.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddLogging(this IAspNetCoreControllersServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext();
        });

        return builder;
    }
}