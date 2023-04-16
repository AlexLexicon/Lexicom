using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.Smtp.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddSmtp(this IAspNetCoreControllersServiceBuilder builder, Action<ISmtpServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomAspNetCoreControllersSmtp(builder.WebApplicationBuilder.Configuration, configure);

        return builder;
    }
}