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

        var provider = new WpfApplicationSettingsProvider(settings);

        builder.Configuration.AddSettings(provider);

        builder.Services.AddLexicomWpfConfigurationSettings(provider);

        return builder;
    }
}
