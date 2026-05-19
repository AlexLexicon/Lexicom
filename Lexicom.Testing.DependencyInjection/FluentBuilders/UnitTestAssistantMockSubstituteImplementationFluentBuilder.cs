using Lexicom.Testing.DependencyInjection.Mocking;

namespace Lexicom.Testing.DependencyInjection;

public class UnitTestAssistantMockSubstituteImplementationFluentBuilder<TService, TImplementation> : IUnitTestAssistantMockSubstituteFluentBuilder<TImplementation> where TService : class where TImplementation : class, TService
{
    /// <exception cref="ArgumentNullException"></exception>
    public UnitTestAssistantMockSubstituteImplementationFluentBuilder(
        MockManager manager,
        MockContainer<TService> container)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(container);

        Manager = manager;
        Container = container;
    }

    protected MockManager Manager { get; }
    protected MockContainer<TService> Container { get; }

    /// <exception cref="ArgumentNullException"></exception>
    public virtual IUnitTestAssistantMockFluentBuilder<TImplementation> So(Action<TImplementation> substitutions)
    {
        ArgumentNullException.ThrowIfNull(substitutions);

        Container.SetConfigureDelegate(substitutions);

        return this;
    }

    public TImplementation Pull()
    {
        return (TImplementation)Manager.Pull(Container.ServiceType);
    }
}
