using Microsoft.Extensions.Configuration;

namespace Lexicom.Configuration.Settings;
public class SettingsConfigurationSource : IConfigurationSource
{
    public IApplicationSettingsProvider? Settings { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return new SettingsConfigurationProvider(Settings);
    }
}
