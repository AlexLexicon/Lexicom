using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Lexicom.Configuration.Settings;
public interface ISettingsWriter
{
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, bool value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, bool? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, byte value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, byte? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, char value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, char? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, decimal value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, decimal? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, double value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, double? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, float value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, float? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, int value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, int? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, long value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, long? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, sbyte value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, sbyte? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, short value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, short? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, string? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, System.Collections.Specialized.StringCollection? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, DateTime value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, DateTime? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, System.Drawing.Color value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, System.Drawing.Color? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, System.Drawing.Point value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, System.Drawing.Point? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, System.Drawing.Size value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, System.Drawing.Size? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, Guid value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, Guid? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, TimeSpan value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, TimeSpan? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, uint value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, uint? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, ulong value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, ulong? value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, ushort value);
    /// <exception cref="ArgumentNullException"/>
    void Save(string key, ushort? value);
    void SaveAndBind<T>(T configuration) where T : class;

    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, bool value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, bool? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, byte value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, byte? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, char value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, char? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, decimal value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, decimal? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, double value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, double? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, float value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, float? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, int value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, int? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, long value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, long? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, sbyte value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, sbyte? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, short value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, short? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, string? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, System.Collections.Specialized.StringCollection? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, DateTime value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, DateTime? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, System.Drawing.Color value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, System.Drawing.Color? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, System.Drawing.Point value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, System.Drawing.Point? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, System.Drawing.Size value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, System.Drawing.Size? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, Guid value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, Guid? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, TimeSpan value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, TimeSpan? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, uint value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, uint? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, ulong value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, ulong? value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, ushort value);
    /// <exception cref="ArgumentNullException"/>
    Task SaveAsync(string key, ushort? value);
    Task SaveAndBindAsync<T>(T configuration) where T : class;
}
public class SettingsWriter : ISettingsWriter
{
    private static void SetConfigPropertyNullableType<T>(Type propertyType, object? value, ref NullableConfigPropertyValue configProperty, T defaultValue = default) where T : struct
    {
        if (!configProperty.IsNullable)
        {
            if (propertyType == typeof(T?))
            {
                var nullableValue = (T?)value;

                object? configValue = defaultValue;

                if (nullableValue.HasValue)
                {
                    configValue = nullableValue.Value;
                }

                configProperty.Value = (T)configValue;
                configProperty.IsNullable = true;
            }
        }
    }

    private readonly ILogger<SettingsWriter> _logger;
    private readonly IApplicationSettingsProvider _settings;

    /// <exception cref="ArgumentNullException"/>
    public SettingsWriter(
        ILogger<SettingsWriter> logger,
        IApplicationSettingsProvider settings)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(settings);

        _logger = logger;
        _settings = settings;
    }

    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, bool value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, bool? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, byte value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, byte? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, char value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, char? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, decimal value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, decimal? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, double value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, double? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, float value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, float? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, int value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, int? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, long value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, long? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, sbyte value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, sbyte? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, short value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, short? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, string? value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, System.Collections.Specialized.StringCollection? value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, DateTime value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, DateTime? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, System.Drawing.Color value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, System.Drawing.Color? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, System.Drawing.Point value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, System.Drawing.Point? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, System.Drawing.Size value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, System.Drawing.Size? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, Guid value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, Guid? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, TimeSpan value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, TimeSpan? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, uint value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, uint? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, ulong value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, ulong? value) => SaveAndSetAsync(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, ushort value) => SaveAndSetAsync(key, value);
    /// <exception cref="ArgumentNullException"/>
    public Task SaveAsync(string key, ushort? value) => SaveAndSetAsync(key, value ?? default);

    public Task SaveAndBindAsync<T>(T? configuration) where T : class
    {
        SaveAndBind(configuration);

        return Task.CompletedTask;
    }

    private Task SaveAndSetAsync(string key, object? value)
    {
        SaveAndSet(key, value);

        return Task.CompletedTask;
    }

    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, bool value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, bool? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, byte value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, byte? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, char value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, char? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, decimal value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, decimal? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, double value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, double? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, float value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, float? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, int value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, int? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, long value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, long? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, sbyte value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, sbyte? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, short value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, short? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, string? value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, System.Collections.Specialized.StringCollection? value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, DateTime value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, DateTime? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, System.Drawing.Color value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, System.Drawing.Color? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, System.Drawing.Point value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, System.Drawing.Point? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, System.Drawing.Size value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, System.Drawing.Size? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, Guid value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, Guid? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, TimeSpan value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, TimeSpan? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, uint value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, uint? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, ulong value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, ulong? value) => SaveAndSet(key, value ?? default);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, ushort value) => SaveAndSet(key, value);
    /// <exception cref="ArgumentNullException"/>
    public void Save(string key, ushort? value) => SaveAndSet(key, value ?? default);

    public void SaveAndBind<T>(T? configuration) where T : class
    {
        if (configuration is not null)
        {
            string configurationTypeName = typeof(T).Name;

            List<ConfigProperty> configProperties = GetConfigProperties(configurationTypeName, configuration);

            foreach (var configProperty in configProperties)
            {
                _settings[configProperty.SettingKey] = configProperty.Value;
            }

            _logger.LogInformation("Saving the settings for the configuraion '{configurationTypeName}'.", configurationTypeName);

            _settings.Save();
        }
    }

    private void SaveAndSet(string key, object? value)
    {
        ArgumentNullException.ThrowIfNull(key);

        _settings[key] = value;

        string logValue;
        if (value is string valueString)
        {
            logValue = $"\"{valueString}\"";
        }
        else
        {
            logValue = value?.ToString() ?? "null";
        }

        _logger.LogInformation("Saving the setting '{key}:{logValue}'.", key, logValue);

        _settings.Save();
    }

    private List<ConfigProperty> GetConfigProperties(string currentPath, object? value, PropertyInfo? propertyInfo = null)
    {
        string settingKey = propertyInfo is null ? currentPath : $"{currentPath}_{propertyInfo.Name}";

        if (propertyInfo is not null)
        {
            Type propertyType = propertyInfo.PropertyType;

            if (propertyType == typeof(bool) ||
                propertyType == typeof(byte) ||
                propertyType == typeof(char) ||
                propertyType == typeof(decimal) ||
                propertyType == typeof(double) ||
                propertyType == typeof(float) ||
                propertyType == typeof(int) ||
                propertyType == typeof(long) ||
                propertyType == typeof(sbyte) ||
                propertyType == typeof(short) ||
                propertyType == typeof(string) ||
                propertyType == typeof(System.Collections.Specialized.StringCollection) ||
                propertyType == typeof(DateTime) ||
                propertyType == typeof(System.Drawing.Color) ||
                propertyType == typeof(System.Drawing.Point) ||
                propertyType == typeof(System.Drawing.Size) ||
                propertyType == typeof(Guid) ||
                propertyType == typeof(TimeSpan) ||
                propertyType == typeof(uint) ||
                propertyType == typeof(ulong) ||
                propertyType == typeof(ushort))
            {
                return new List<ConfigProperty>
                {
                    new ConfigProperty
                    {
                        SettingKey = settingKey,
                        Value = value,
                    }
                };
            }

            var nullableConfigPropertyValue = new NullableConfigPropertyValue();
            SetConfigPropertyNullableType<bool>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<byte>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<char>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<decimal>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<double>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<float>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<int>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<long>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<sbyte>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<short>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<DateTime>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<Guid>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<TimeSpan>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<uint>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<ulong>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<ushort>(propertyType, value, ref nullableConfigPropertyValue);

            if (nullableConfigPropertyValue.IsNullable)
            {
                return new List<ConfigProperty>
                {
                    new ConfigProperty
                    {
                        SettingKey = settingKey,
                        Value = nullableConfigPropertyValue.Value,
                    }
                };
            }
        }

        if (value is not null)
        {
            Type valueType = value.GetType();

            var valueConfigProperties = new List<ConfigProperty>();
            foreach (PropertyInfo valuePropertyInfo in valueType.GetProperties())
            {
                object? valueValue = valuePropertyInfo.GetValue(value);

                var configProperties = GetConfigProperties(settingKey, valueValue, valuePropertyInfo);

                valueConfigProperties.AddRange(configProperties);
            }

            return valueConfigProperties;
        }

        return new List<ConfigProperty>();
    }

    private class NullableConfigPropertyValue
    {
        public NullableConfigPropertyValue()
        {
            IsNullable = false;
        }

        public bool IsNullable { get; set; }
        public object? Value { get; set; }
    }

    private class ConfigProperty
    {
        public required string SettingKey { get; set; }
        public required object? Value { get; set; }
    }
}