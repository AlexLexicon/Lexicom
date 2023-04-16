using Microsoft.AspNetCore.Builder;

namespace Lexicom.AspNetCore.Controllers.Amenities;
public interface IAspNetCoreControllersAmenitiesServiceBuilder
{
    WebApplicationBuilder WebApplicationBuilder { get; }
}
public class AspNetCoreControllersAmenitiesServiceBuilder : IAspNetCoreControllersAmenitiesServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AspNetCoreControllersAmenitiesServiceBuilder(WebApplicationBuilder webApplicationBuilder)
    {
        ArgumentNullException.ThrowIfNull(webApplicationBuilder);

        WebApplicationBuilder = webApplicationBuilder;
    }

    public WebApplicationBuilder WebApplicationBuilder { get; }
}
