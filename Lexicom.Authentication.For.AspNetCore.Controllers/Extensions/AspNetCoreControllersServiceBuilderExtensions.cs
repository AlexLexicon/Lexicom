using Lexicom.Authentication.Extensions;
using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddAuthentication(this IAspNetCoreControllersServiceBuilder builder, Action<IAuthenticationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomAuthentication(builder.Configuration, configure);

        return builder;
    }
}
