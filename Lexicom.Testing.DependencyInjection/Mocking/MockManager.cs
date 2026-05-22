using Lexicom.Testing.DependencyInjection.Exceptions;
using Lexicom.Testing.DependencyInjection.Utility;
using NSubstitute;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Lexicom.Testing.DependencyInjection.Mocking;

public class MockManager : IDisposable, IReadOnlyDictionary<Type, MockContainer>
{
    public MockManager(ITestAssistant testAssistant)
    {
        TestAssistant = testAssistant;

        MockTypeToContainer = [];
    }

    private Dictionary<Type, MockContainer> MockTypeToContainer { get; }

    public ITestAssistant TestAssistant { get; }
    public IEnumerable<Type> Keys => MockTypeToContainer.Keys;
    public IEnumerable<MockContainer> Values => MockTypeToContainer.Values;
    public int Count => MockTypeToContainer.Count;

    public MockContainer this[Type key] => MockTypeToContainer[key];

    public void Dispose()
    {
        foreach (MockContainer container in MockTypeToContainer.Values)
        {
            container?.Dispose();
        }
    }

    public UnitTestAssistantMockFluentBuilder<TService> Mock<TService>() where TService : class
    {
        return Mock<TService>(TestAssistant.AssistantConfiguration.DefaultMockLifetime);
    }
    public UnitTestAssistantMockFluentBuilder<TService> Mock<TService>(MockLifetime lifetime) where TService : class
    {
        var container = new MockContainer<TService>(this, lifetime);

        MockTypeToContainer.Add(container.ServiceType, container);

        return new UnitTestAssistantMockFluentBuilder<TService>(this, container);
    }
    internal UnitTestAssistantMockFluentBuilder Mock(Type serviceType, MockLifetime lifetime)
    {
        var container = new MockContainer(this, serviceType, lifetime);

        MockTypeToContainer.Add(container.ServiceType, container);

        return new UnitTestAssistantMockFluentBuilder(this, container);
    }

    /// <exception cref="PullValueTypeException"/>
    /// <exception cref="PullNotMockedException"/>
    public object Pull(Type type)
    {
        if (TestAssistant.AssistantConfiguration.IsAutomaticallyMocking)
        {
            if (!MockTypeToContainer.ContainsKey(type))
            {
                if (TypeUtilities.IsValueType(type))
                {
                    throw new PullValueTypeException(type);
                }

                if (!MockHooksManager.TryToMockFromHooks(this, type))
                {
                    Mock(type, TestAssistant.AssistantConfiguration.DefaultMockLifetime);
                }
            }
        }

        if (!MockTypeToContainer.TryGetValue(type, out MockContainer? container))
        {
            throw new PullNotMockedException(type);
        }

        return container.Pull();
    }

    public object CreateSubstitute(Type substituteType)
    {
        ConstructorInfo[] constructors = substituteType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

        //we go ahead and create substitutes for all the required constructor parameters for this substitute this
        //only applies when this substitute is a concrete type and not an interface which should be rare hopefully
        object[] constructorArguments = [];
        if (constructors.Length > 0)
        {
            ParameterInfo[] parameters = constructors
                .First()
                .GetParameters();

            constructorArguments = new object[parameters.Length];
            for (int parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                ParameterInfo parameter = parameters[parameterIndex];
                Type parameterType = parameter.ParameterType;

                object instance = Pull(parameterType);

                constructorArguments[parameterIndex] = instance;
            }
        }

        return Substitute.For(typesToProxy: [substituteType], constructorArguments);
    }

    public bool ContainsKey(Type key) => MockTypeToContainer.ContainsKey(key);
    public bool TryGetValue(Type key, [MaybeNullWhen(false)] out MockContainer value) => MockTypeToContainer.TryGetValue(key, out value);
    public IEnumerator<KeyValuePair<Type, MockContainer>> GetEnumerator() => MockTypeToContainer.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}