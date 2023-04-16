using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddAmenities(this IAspNetCoreControllersServiceBuilder builder, Action<IAspNetCoreControllersAmenitiesServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.AddLexicomAspNetCoreControllersAmenities(configure);

        return builder;
    }
}