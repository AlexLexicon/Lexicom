using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Lexicom.Configuration.Settings;
public class SettingsConfigurationProvider : ConfigurationProvider
{
    private readonly ILogger<SettingsConfigurationProvider> _logger;
    private readonly IApplicationSettingsProvider? _settings;

    public SettingsConfigurationProvider(
        ILogger<SettingsConfigurationProvider> logger,
        IApplicationSettingsProvider? settings)
    {
        _logger = logger;
        _settings = settings;

        if (_settings is not null)
        {
            _settings.SettingsSaving += (sender, e) => Load();
        }
    }

    public override void Load()
    {
        if (_settings is not null)
        {
            bool reload = false;

            foreach (ISettingsProperty setting in _settings.Properties)
            {
                string propertyName = setting.Name;
                string dataKey = propertyName.Replace('_', ':');
                string? dataValue = _settings[propertyName]?.ToString();

                if (Data.TryGetValue(dataKey, out string? value))
                {
                    if (value != dataValue)
                    {
                        Data[dataKey] = dataValue;
                        reload = true;
                    }
                }
                else
                {
                    Data.Add(dataKey, dataValue);
                    reload = true;
                }

                _logger.LogInformation("Loaded the setting '{dataKey}:{dataValue}' from the property '{propertyName}'.", dataKey, dataValue?.ToString() ?? "null", propertyName);
            }

            if (reload)
            {
                OnReload();
            }
        }
        else
        {
            _logger.LogWarning("Could not load any settings because the '{settingsProvider}' was null.", nameof(IApplicationSettingsProvider));
        }
    }
}
