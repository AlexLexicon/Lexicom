using Lexicom.Testing.DependencyInjection.Mocking;

namespace Lexicom.Testing.DependencyInjection;

public interface IUnitTestAssistantMockFluentBuilder
{
    object Pull();
}
public class UnitTestAssistantMockFluentBuilder : IUnitTestAssistantMockFluentBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public UnitTestAssistantMockFluentBuilder(
        MockManager manager,
        MockContainer container)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(container);

        Manager = manager;
        Container = container;
    }

    protected MockManager Manager { get; }
    protected MockContainer Container { get; }

    /// <exception cref="ArgumentNullException"/>
    public void With(Type implementationType)
    {
        ArgumentNullException.ThrowIfNull(implementationType);

        Container.Instantiater.Set(implementationType);
    }

    public object Pull()
    {
        return Manager.Pull(Container.ServiceType);
    }
}
public interface IUnitTestAssistantMockFluentBuilder<TService>
{
    TService Pull();
}
public interface IUnitTestAssistantMockSubstituteFluentBuilder<TService> : IUnitTestAssistantMockFluentBuilder<TService> where TService : class
{
    /// <exception cref="ArgumentNullException"/>
    IUnitTestAssistantMockFluentBuilder<TService> So(Action<TService> substitutions);
}
public class UnitTestAssistantMockFluentBuilder<TService> : UnitTestAssistantMockFluentBuilder, IUnitTestAssistantMockSubstituteFluentBuilder<TService> where TService : class
{
    /// <exception cref="ArgumentNullException"/>
    public UnitTestAssistantMockFluentBuilder(
        MockManager manager, 
        MockContainer<TService> container) 
        : base(
            manager, 
            container)
    {
        ArgumentNullException.ThrowIfNull(container);

        GenericContainer = container;
    }

    protected MockContainer<TService> GenericContainer { get; }

    /// <exception cref="ArgumentNullException"/>
    public IUnitTestAssistantMockFluentBuilder<TService> So(Action<TService> substitutions)
    {
        ArgumentNullException.ThrowIfNull(substitutions);

        GenericContainer.SetConfigureDelegate(substitutions);

        return this;
    }

    public IUnitTestAssistantMockFluentBuilder<TImplementation> With<TImplementation>() where TImplementation : class, TService
    {
        Container.Instantiater.Set(typeof(TImplementation));

        return new UnitTestAssistantMockSubstituteImplementationFluentBuilder<TService, TImplementation>(Manager, GenericContainer);
    }

    public IUnitTestAssistantMockFluentBuilder<TService> With<TImplementation>(TImplementation instance) where TImplementation : class, TService
    {
        Container.Instantiater.Set(instance);

        return this;
    }

    public IUnitTestAssistantMockFluentBuilder<TService> With<TImplementation>(Func<TImplementation> factory) where TImplementation : class, TService
    {
        Container.Instantiater.Set(factory);

        return this;
    }

    public new TService Pull()
    {
        return (TService)base.Pull();
    }
}
