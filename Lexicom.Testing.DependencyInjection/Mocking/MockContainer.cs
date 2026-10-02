namespace Lexicom.Testing.DependencyInjection.Mocking;

public class MockContainer : IDisposable
{
    /// <exception cref="ArgumentNullException"/>
    public MockContainer(
        MockManager manager,
        Type serviceType,
        MockLifetime lifetime)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(serviceType);

        ServiceType = serviceType;
        Lifetime = lifetime;

        Instantiations = [];
        Instantiater = new MockInstantiater(manager, ServiceType);
    }

    public Type ServiceType { get; }
    public MockInstantiater Instantiater { get; }

    protected MockLifetime Lifetime { get; }
    protected HashSet<object> Instantiations { get; }

    public void Dispose()
    {
        Instantiater?.Dispose();

        foreach (object instantiation in Instantiations)
        {
            if (instantiation is IDisposable disposableInstantiation)
            {
                disposableInstantiation.Dispose();
            }
        }
    }

    public object Pull()
    {
        object instance;
        if (Lifetime == MockLifetime.Singleton && Instantiations.Count > 0)
        {
            instance = Instantiations.First();
        }
        else
        {
            instance = Instantiater.CreateInstance();

            ConfigureInstance(instance);

            Instantiations.Add(instance);
        }

        return instance;
    }
    public T Pull<T>()
    {
        return (T)Pull();
    }

    protected virtual void ConfigureInstance(object instance)
    {
    }
}
public class MockContainer<TService> : MockContainer where TService : class
{
    public MockContainer(
        MockManager manager,
        MockLifetime lifetime)
        : base(
            manager,
            serviceType: typeof(TService),
            lifetime)
    {
    }

    /// <exception cref="ArgumentNullException"/>
    public void SetConfigureDelegate<TImplementation>(Action<TImplementation> substitutions) where TImplementation : class, TService
    {
        ArgumentNullException.ThrowIfNull(substitutions);

        SubstitutionsDelegate = value => substitutions((TImplementation)value);
    }

    protected Action<object>? SubstitutionsDelegate { get; set; }

    protected override void ConfigureInstance(object instance)
    {
        SubstitutionsDelegate?.Invoke(instance);
    }
}
