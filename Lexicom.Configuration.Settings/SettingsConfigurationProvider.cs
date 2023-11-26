using Microsoft.Extensions.Configuration;

namespace Lexicom.Configuration.Settings;
public class SettingsConfigurationProvider : ConfigurationProvider
{
    private readonly IApplicationSettingsProvider? _settings;

    public SettingsConfigurationProvider(IApplicationSettingsProvider? settings)
    {
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
            }

            if (reload)
            {
                OnReload();
            }
        }
    }
}
