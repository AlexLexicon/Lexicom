using Lexicom.Supports.AspNetCore.Controllers;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddValidation(this IAspNetCoreControllersServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddValidation(builder, null);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddValidation(this IAspNetCoreControllersServiceBuilder builder, Action<IValidationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomValidation(builder.WebApplicationBuilder.Configuration, configure);

        return builder;
    }
}
