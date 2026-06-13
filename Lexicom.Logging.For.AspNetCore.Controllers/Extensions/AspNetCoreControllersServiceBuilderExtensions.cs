using Lexicom.Supports.AspNetCore.Controllers;
using Serilog;

namespace Lexicom.Logging.For.AspNetCore.Controllers.Extensions;

public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IDependentAspNetCoreControllersServiceBuilder AddLogging(this IDependentAspNetCoreControllersServiceBuilder builder)
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