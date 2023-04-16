using Microsoft.AspNetCore.Builder;

namespace Lexicom.Supports.AspNetCore.Controllers.Extensions;
public static class WebApplicationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WebApplicationBuilder Lexicom(this WebApplicationBuilder builder, Action<IAspNetCoreControllersServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new AspNetCoreControllersServiceBuilder(builder));

        return builder;
    }
}