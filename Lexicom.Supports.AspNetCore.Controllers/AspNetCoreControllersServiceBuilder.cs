using Microsoft.AspNetCore.Builder;

namespace Lexicom.Supports.AspNetCore.Controllers;
public interface IAspNetCoreControllersServiceBuilder
{
    WebApplicationBuilder WebApplicationBuilder { get; }
}
public class AspNetCoreControllersServiceBuilder : IAspNetCoreControllersServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AspNetCoreControllersServiceBuilder(WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        WebApplicationBuilder = builder;
    }

    public WebApplicationBuilder WebApplicationBuilder { get; }
}
