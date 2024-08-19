using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Collections;

namespace Lexicom.UnitTesting;
public class UnitTestAttendant : IServiceCollection
{
    private readonly IServiceCollection _services;
    private readonly Dictionary<Type, Func<object>> _mocks;

    public UnitTestAttendant()
    {
        _services = new ServiceCollection();
        _mocks = [];

        Configuration = new ConfigurationManager();
    }

    public int Count => _services.Count;
    public bool IsReadOnly => _services.IsReadOnly;
    public ConfigurationManager Configuration { get; }
    private IServiceProvider? Provider { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public void Mock<T>(Action<T> configure) where T : class
    {
        ArgumentNullException.ThrowIfNull(configure);

        Type mockType = typeof(T);

        if (_mocks.ContainsKey(mockType))
        {
            _mocks[mockType] = genericMockFactory;
        }
        else
        {
            _mocks.Add(mockType, genericMockFactory);
        }

        _services.AddSingleton(sp => (T)_mocks[typeof(T)].Invoke());

        T genericMockFactory()
        {
            var mock = Substitute.For<T>();

            configure.Invoke(mock);

            return mock;
        }
    }

    /// <exception cref="InvalidOperationException"/>
    public T Get<T>() where T : class
    {
        if (Provider is null)
        {
            _services.AddSingleton<IConfiguration>(Configuration);

            Provider = _services.BuildServiceProvider();
        }

        return Provider.GetRequiredService<T>();
    }

    public ServiceDescriptor this[int index]
    {
        get => _services[index];
        set => _services[index] = value;
    }

    public int IndexOf(ServiceDescriptor item) => _services.IndexOf(item);

    public bool Contains(ServiceDescriptor item) => _services.Contains(item);

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="ArgumentException"/>
    public void CopyTo(ServiceDescriptor[] array, int arrayIndex) => _services.CopyTo(array, arrayIndex);

    /// <exception cref="NotSupportedException"/>
    public void Clear() => _services.Clear();

    /// <exception cref="NotSupportedException"/>
    public void Add(ServiceDescriptor item) => _services.Add(item);

    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="NotSupportedException"/>
    public void Insert(int index, ServiceDescriptor item) => _services.Insert(index, item);

    /// <exception cref="NotSupportedException"/>
    public bool Remove(ServiceDescriptor item) => _services.Remove(item);

    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="NotSupportedException"/>
    public void RemoveAt(int index) => _services.RemoveAt(index);

    public IEnumerator<ServiceDescriptor> GetEnumerator() => _services.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
