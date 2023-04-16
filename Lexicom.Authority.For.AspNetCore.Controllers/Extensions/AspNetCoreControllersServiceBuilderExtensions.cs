using Lexicom.Authority.Extensions;
using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.Authority.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddAuthority(this IAspNetCoreControllersServiceBuilder builder, Action<IAuthorityServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomAuthority(configure);

        return builder;
    }
}
