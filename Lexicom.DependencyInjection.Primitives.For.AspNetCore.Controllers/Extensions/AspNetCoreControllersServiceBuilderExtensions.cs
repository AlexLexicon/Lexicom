using Lexicom.Supports.AspNetCore.Controllers;
using Lexicom.DependencyInjection.Primitives.Extensions;

namespace Lexicom.DependencyInjection.Primitives.For.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddTimeProvider(this IAspNetCoreControllersServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomTimeProvider();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddGuidProvider(this IAspNetCoreControllersServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomGuidProvider();

        return builder;
    }
}
