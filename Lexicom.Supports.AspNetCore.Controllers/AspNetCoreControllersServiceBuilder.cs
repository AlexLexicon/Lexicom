using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Supports.AspNetCore.Controllers;

public interface IAspNetCoreControllersServiceBuilder
{
    IServiceCollection Services { get; }
    ConfigurationManager Configuration { get; }
}
public interface IDependentAspNetCoreControllersServiceBuilder : IAspNetCoreControllersServiceBuilder
{
    WebApplicationBuilder WebApplicationBuilder { get; }
}
public class AspNetCoreControllersServiceBuilder : IDependentAspNetCoreControllersServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AspNetCoreControllersServiceBuilder(WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        WebApplicationBuilder = builder;
    }

    public WebApplicationBuilder WebApplicationBuilder { get; }
    public IServiceCollection Services => WebApplicationBuilder.Services;
    public ConfigurationManager Configuration => WebApplicationBuilder.Configuration;
}
