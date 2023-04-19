using Lexicom.Supports.AspNetCore.Controllers;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddValidation(this IAspNetCoreControllersServiceBuilder builder, Action<IValidationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomValidation(configure);

        return builder;
    }
}
