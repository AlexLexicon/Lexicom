using System.ComponentModel;

namespace Lexicom.Configuration.Settings;
public interface IApplicationSettingsProvider
{
    delegate void SettingsSavingEventHandler(object sender, CancelEventArgs e);

    public event SettingsSavingEventHandler? SettingsSaving;

    object? this[string propertyName] { get; set; }
    IReadOnlyList<ISettingsProperty> Properties { get; }

    void Save();
}
