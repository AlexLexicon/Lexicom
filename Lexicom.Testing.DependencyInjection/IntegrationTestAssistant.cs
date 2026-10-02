using Lexicom.Testing.DependencyInjection.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections;
using System.Reflection;

namespace Lexicom.Testing.DependencyInjection;

public interface IIntegrationTestAssistant : ITestAssistant, IServiceCollection, IServiceProvider
{
    ConfigurationManager Configuration { get; }
    bool IsMakingInstance { get; }
}
public class IntegrationTestAssistant : TestAssistant, IIntegrationTestAssistant
{
    private readonly IServiceCollection _services;

    public IntegrationTestAssistant() : this(new TestAssistantConfiguration())
    {
    }
    public IntegrationTestAssistant(TestAssistantConfiguration configuration) : base(configuration)
    {
        _services = new ServiceCollection();

        Configuration = new ConfigurationManager();
    }

    public override TestingCategory Category => TestingCategory.IntegrationTest;
    public int Count => _services.Count;
    public bool IsReadOnly => _services.IsReadOnly;
    public bool IsMakingInstance { get; private set; }
    public ConfigurationManager Configuration { get; }
    private bool BuildNewProvider { get; set; }
    private CascadingServiceProvider? InternalProvider { get; set; }

    public ServiceDescriptor this[int index]
    {
        get => _services[index];
        set => _services[index] = value;
    }

    protected override Type GetMakeType(Type serviceType)
    {
        IsTypeRegistered(serviceType, out ServiceDescriptor? serviceDescriptor);

        Type makeType;
        if (serviceType.IsInterface)
        {
            if (serviceDescriptor is null)
            {
                throw new MakeTypeIsNotRegisteredException(serviceType);
            }

            if (serviceDescriptor.ImplementationType is null)
            {
                throw new NotImplementedException("Need to implement handler for factory?");
            }

            makeType = serviceDescriptor.ImplementationType;
        }
        else
        {
            makeType = serviceType;

            if (serviceDescriptor is null)
            {
                BuildNewProvider = true;
                _services.Add(new ServiceDescriptor(serviceType, serviceType, ServiceLifetime.Singleton));
            }
        }

        return makeType;
    }

    protected override void ResolveParameter(int parameterIndex, object[] resolvedParameters, Type parameterType)
    {
        RegisterServiceTypeWhenNotRegistered(parameterType);
    }

    protected bool IsTypeRegistered(Type serviceType, out ServiceDescriptor? serviceDescriptor)
    {
        serviceDescriptor = _services.FirstOrDefault(sd => sd.ServiceType == serviceType);

        if (serviceDescriptor is not null)
        {
            return true;
        }

        if (serviceType.IsConstructedGenericType)
        {
            var openGeneric = serviceType.GetGenericTypeDefinition();

            if (openGeneric == typeof(IEnumerable<>))
            {
                return true;
            }

            serviceDescriptor = _services.FirstOrDefault(sd => sd.ServiceType == openGeneric);

            return true;
        }

        return false;
    }

    protected void RegisterServiceTypeWhenNotRegistered(Type serviceType)
    {
        bool isRegisterd = IsTypeRegistered(serviceType, out ServiceDescriptor? existingServiceDescriptor);
        if (!isRegisterd && existingServiceDescriptor is null)
        {
            BuildNewProvider = true;
            _services.Add(new ServiceDescriptor(serviceType, sp =>
            {
                return PullAndEnhanceInstance(serviceType);
            }, ServiceLifetime.Singleton));
        }
    }

    protected override object MakeInstance(Type type, Type makeType, ConstructorInfo constructor, object[] resolvedParameters)
    {
        //for integration tests the resolved parameters are only used for manually provided parameters
        object[] manualParameters = resolvedParameters
            .Where(rp => rp is not null)
            .ToArray();

        IsMakingInstance = true;

        if (manualParameters.Length > 0)
        {
            return ActivatorUtilities.CreateInstance(this, makeType, manualParameters);
        }

        object instance = ServiceProviderServiceExtensions.GetRequiredService(this, type);

        IsMakingInstance = false;

        return instance;
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

    public object? GetService(Type serviceType)
    {
        RegisterServiceTypeWhenNotRegistered(serviceType);

        if (BuildNewProvider || InternalProvider is null)
        {
            _services.TryAddSingleton<IntegrationTestAssistant>(this);
            _services.TryAddSingleton<IIntegrationTestAssistant>(sp =>
            {
                return sp.GetRequiredService<IntegrationTestAssistant>();
            });
            _services.TryAddSingleton<ITestAssistant>(sp =>
            {
                return sp.GetRequiredService<IIntegrationTestAssistant>();
            });
            _services.TryAddSingleton<IConfiguration>(Configuration);
            _services.TryAddSingleton<IServiceProviderIsService>(sp =>
            {
                return sp.GetRequiredService<IServiceProviderIsService>();
            });

            InternalProvider = new CascadingServiceProvider(_services, InternalProvider);
        }

        try
        {
            return InternalProvider.GetService(serviceType);
        }
        catch (ArgumentException e)
        {
            if (e.Message.StartsWith("Can not create proxy for type "))
            {
                return null;
            }

            return null;
        }
    }
}
