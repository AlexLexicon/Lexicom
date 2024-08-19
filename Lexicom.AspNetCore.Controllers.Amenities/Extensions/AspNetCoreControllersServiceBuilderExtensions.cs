using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IDependantAspNetCoreControllersServiceBuilder AddAmenities(this IDependantAspNetCoreControllersServiceBuilder builder, Action<IAspNetCoreControllersAmenitiesServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.AddLexicomAspNetCoreControllersAmenities(configure);

        return builder;
    }
}