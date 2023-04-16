using Lexicom.Cryptography.Extensions;
using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.Cryptography.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddCryptography(this IAspNetCoreControllersServiceBuilder builder, Action<ICryptographyServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomCryptography(configure);

        return builder;
    }
}
