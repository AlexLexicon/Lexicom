using System.Collections;
using System.Collections.Specialized;
using System.Reflection;

namespace Lexicom.DependencyInjection;
public class ConfigurationInMemorySource : IEnumerable<KeyValuePair<string, string?>>
{
    protected readonly Dictionary<string, string?> _source = new Dictionary<string, string?>();

    /// <exception cref="ArgumentNullException"/>
    public void Add(string key, string? value)
    {
        ArgumentNullException.ThrowIfNull(key);

        _source.Add(key, value);
    }
    public void Add(object? configuration)
    {
        if (configuration is not null)
        {
            Type configType = configuration.GetType();

            var key = new KeyStringBuilder(configType.Name);

            AddProperties(key, configType, configuration);
        }
    }

    private void AddProperties(KeyStringBuilder key, Type? objType, object? obj)
    {
        if (objType is null || obj is null)
        {
            Add(key, null);

            return;
        }

        bool isNullableType = false;
        AddNullableType<bool>(key, objType, obj, ref isNullableType);
        AddNullableType<byte>(key, objType, obj, ref isNullableType);
        AddNullableType<char>(key, objType, obj, ref isNullableType);
        AddNullableType<decimal>(key, objType, obj, ref isNullableType);
        AddNullableType<double>(key, objType, obj, ref isNullableType);
        AddNullableType<float>(key, objType, obj, ref isNullableType);
        AddNullableType<int>(key, objType, obj, ref isNullableType);
        AddNullableType<long>(key, objType, obj, ref isNullableType);
        AddNullableType<sbyte>(key, objType, obj, ref isNullableType);
        AddNullableType<short>(key, objType, obj, ref isNullableType);
        AddNullableType<DateTime>(key, objType, obj, ref isNullableType);
        AddNullableType<Guid>(key, objType, obj, ref isNullableType);
        AddNullableType<TimeSpan>(key, objType, obj, ref isNullableType);
        AddNullableType<uint>(key, objType, obj, ref isNullableType);
        AddNullableType<ulong>(key, objType, obj, ref isNullableType);
        AddNullableType<ushort>(key, objType, obj, ref isNullableType);

        if (isNullableType)
        {
            return;
        }

        if (objType == typeof(bool) ||
            objType == typeof(byte) ||
            objType == typeof(char) ||
            objType == typeof(decimal) ||
            objType == typeof(double) ||
            objType == typeof(float) ||
            objType == typeof(int) ||
            objType == typeof(long) ||
            objType == typeof(sbyte) ||
            objType == typeof(short) ||
            objType == typeof(string) ||
            objType == typeof(StringCollection) ||
            objType == typeof(DateTime) ||
            objType == typeof(Guid) ||
            objType == typeof(TimeSpan) ||
            objType == typeof(uint) ||
            objType == typeof(ulong) ||
            objType == typeof(ushort))
        {
            Add(key, obj?.ToString());

            return;
        }

        if (obj is IEnumerable enumerableValue)
        {
            var enumerator = enumerableValue.GetEnumerator();

            int index = 0;
            while (enumerator.MoveNext())
            {
                object? item = enumerator.Current;

                KeyStringBuilder itemKey = key.Build(index.ToString());
                AddProperties(itemKey, item?.GetType(), item);

                index++;
            }

            return;
        }

        foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
        {
            KeyStringBuilder subKey = key.Build(propertyInfo.Name);

            Type propertyType = propertyInfo.PropertyType;
            object? value = propertyInfo.GetValue(obj);

            AddProperties(subKey, propertyType, value);
        }
    }

    private void AddNullableType<T>(KeyStringBuilder key, Type propertyType, object? value, ref bool isNullableType) where T : struct
    {
        if (!isNullableType && propertyType == typeof(T?))
        {
            var nullableValue = (T?)value;
            string? stringValue = nullableValue.HasValue ? nullableValue.Value.ToString() : null;

            Add(key, stringValue);

            isNullableType = true;
        }
    }

    public IEnumerator<KeyValuePair<string, string?>> GetEnumerator() => _source.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private class KeyStringBuilder
    {
        /// <exception cref="ArgumentNullException"/>
        public static implicit operator string(KeyStringBuilder keyStringBuilder)
        {
            ArgumentNullException.ThrowIfNull(keyStringBuilder);

            return keyStringBuilder.ToString();
        }

        private readonly string _key;

        /// <exception cref="ArgumentNullException"/>
        public KeyStringBuilder(string root)
        {
            ArgumentNullException.ThrowIfNull(root);

            _key = root;
        }

        /// <exception cref="ArgumentNullException"/>
        public KeyStringBuilder Build(string path)
        {
            ArgumentNullException.ThrowIfNull(path);

            return Build(new[]
            {
                path
            });
        }
        /// <exception cref="ArgumentNullException"/>
        public KeyStringBuilder Build(params string[] paths)
        {
            ArgumentNullException.ThrowIfNull(paths);

            string extendedKey = _key;

            foreach (string path in paths)
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    extendedKey += $":{path}";
                }
            }

            return new KeyStringBuilder(extendedKey);
        }

        public override string ToString()
        {
            return _key;
        }
    }
}
