using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.Smtp.AspNetCore.Controllers.Extensions;
public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddSmtp(this IAspNetCoreControllersServiceBuilder builder, Action<ISmtpServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomAspNetCoreControllersSmtp(builder.Configuration, configure);

        return builder;
    }
}