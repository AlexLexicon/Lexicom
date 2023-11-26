using System.Configuration;

namespace Lexicom.Configuration.Settings.For.Wpf;
public class WpfApplicationSettingsProvider : IApplicationSettingsProvider
{
    public event IApplicationSettingsProvider.SettingsSavingEventHandler? SettingsSaving;

    private readonly ApplicationSettingsBase _settings;

    /// <exception cref="ArgumentNullException"/>
    public WpfApplicationSettingsProvider(ApplicationSettingsBase settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        _settings = settings;

        _settings.SettingsSaving += (s, e) => SettingsSaving?.Invoke(s, e);
    }

    public IReadOnlyList<ISettingsProperty> Properties
    {
        get
        {
            List<ISettingsProperty> properties = [];
            foreach (object? setting in _settings.Properties)
            {
                if (setting is SettingsProperty settingsProperty)
                {
                    properties.Add(new SettingsPropertyWrapper(settingsProperty));
                }
            }
            return properties;
        }
    }
    public object? this[string proeprtyName]
    {
        get => _settings[proeprtyName];
        set
        {
            try
            {
                _settings[proeprtyName] = value;
            }
            catch (SettingsPropertyNotFoundException)
            {
                //we dont require that the setting key exists when we do a set
                //instead we just ignore that set and move on
            }
        }
    }

    public void Save()
    {
        _settings.Save();
    }

    private class SettingsPropertyWrapper : ISettingsProperty
    {
        private readonly SettingsProperty _settingsProperty;

        public SettingsPropertyWrapper(SettingsProperty settingsProperty)
        {
            _settingsProperty = settingsProperty;

            Name = _settingsProperty.Name;
        }

        public string Name { get; }
    }
}
