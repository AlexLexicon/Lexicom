using Lexicom.Configuration.Settings.Extensions;
using Lexicom.Supports.Wpf;
using System.Configuration;

namespace Lexicom.Configuration.Settings.For.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddSettings(this IWpfServiceBuilder builder, ApplicationSettingsBase settings)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(settings);

        builder.WpfApplicationBuilder.Configuration.AddSettings(new WpfApplicationSettingsProvider(settings));

        builder.WpfApplicationBuilder.Services.AddLexicomWpfConfigurationSettings(new WpfApplicationSettingsProvider(settings));

        return builder;
    }
}
