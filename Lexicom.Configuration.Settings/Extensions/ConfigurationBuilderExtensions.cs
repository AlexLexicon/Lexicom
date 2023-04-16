using Microsoft.Extensions.Configuration;

namespace Lexicom.Configuration.Settings.Extensions;
public static class ConfigurationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConfigurationBuilder AddSettings(this IConfigurationBuilder builder, IApplicationSettingsProvider applicationSettingsProvider)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(applicationSettingsProvider);

        builder.Add<SettingsConfigurationSource>(source =>
        {
            source.Settings = applicationSettingsProvider;
        });

        return builder;
    }
}
