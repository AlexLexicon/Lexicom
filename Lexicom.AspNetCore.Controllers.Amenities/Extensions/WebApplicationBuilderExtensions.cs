using Microsoft.AspNetCore.Builder;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class WebApplicationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WebApplicationBuilder AddLexicomAspNetCoreControllersAmenities(this WebApplicationBuilder builder, Action<IAspNetCoreControllersAmenitiesServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        configure?.Invoke(new AspNetCoreControllersAmenitiesServiceBuilder(builder));

        return builder;
    }
}
