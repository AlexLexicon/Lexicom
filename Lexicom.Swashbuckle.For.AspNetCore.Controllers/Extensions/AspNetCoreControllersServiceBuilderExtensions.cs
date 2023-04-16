using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.Swashbuckle.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddSwaggerGen(this IAspNetCoreControllersServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomSwaggerGen();

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddSwaggerGen(this IAspNetCoreControllersServiceBuilder builder, SwaggerSettings? settings)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomSwaggerGen(settings);

        return builder;
    }
}
