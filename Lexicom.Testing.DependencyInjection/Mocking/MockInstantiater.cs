namespace Lexicom.Testing.DependencyInjection.Mocking;

public class MockInstantiater : IDisposable
{
    public MockInstantiater(
        MockManager manager,
        Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(serviceType);

        Manager = manager;
        ServiceType = serviceType;
    }

    private MockManager Manager { get; }
    private Type ServiceType { get; }
    private object? Instance { get; set; }
    private Func<object>? Factory { get; set; }
    private Type? ImplementationType { get; set; }

    public void Dispose()
    {
        if (Instance is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public object CreateInstance()
    {
        if (Instance is not null)
        {
            return Instance;
        }

        if (Factory is not null)
        {
            return Factory.Invoke();
        }

        Type substituteType;
        if (ImplementationType is not null)
        {
            substituteType = ImplementationType;
        }
        else
        {
            substituteType = ServiceType;
        }

        return Manager.CreateSubstitute(substituteType);
    }

    public void Set(object instance)
    {
        Instance = instance;
    }
    public void Set(Func<object> factory)
    {
        Factory = factory;
    }
    public void Set(Type implementationType)
    {
        ImplementationType = implementationType;
    }
}
