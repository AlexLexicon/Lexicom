using Lexicom.Smtp;
using Lexicom.Smtp.Extensions;
using Lexicom.Supports.Wpf;

namespace Lexicom.Validation.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddSmtp(this IWpfServiceBuilder builder, Action<ISmtpServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WpfApplicationBuilder.Services.AddLexicomSmtp(builder.WpfApplicationBuilder.Configuration, configure);

        return builder;
    }
}
