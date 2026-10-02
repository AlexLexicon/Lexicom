using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.DependencyInjection.Primitives.For.AspNetCore.Controllers.Extensions;

public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddPrimitives(this IAspNetCoreControllersServiceBuilder builder, Action<IDependencyInjectionPrimitivesServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomDependencyInjectionPrimitives(configure);

        return builder;
    }
}
