using Lexicom.Authorization.Extensions;
using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.Authorization.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddAuthorization(this IAspNetCoreControllersServiceBuilder builder, Action<IAuthorizationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomAuthorization(builder.Configuration, configure);

        return builder;
    }
}
