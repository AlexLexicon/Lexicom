using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Lexicom.Testing.DependencyInjection.Exceptions;
using Lexicom.Testing.DependencyInjection.Utility;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Lexicom.Testing.DependencyInjection.Mocking;

public class MockManager : IDisposable, IReadOnlyDictionary<Type, MockContainer>
{
    private static MethodInfo? _arrayEmptyMethodInfo;
    private static MethodInfo ArrayEmptyMethodInfo => _arrayEmptyMethodInfo ??= typeof(Array).GetMethod(nameof(Array.Empty), BindingFlags.Static | BindingFlags.Public) ?? throw new UnreachableException($"The method '{nameof(Array.Empty)}' was not found.");

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
    private void MockEnumerable(Type enumerableType, Type genericArgumentType)
    {
        //the enumerable is transient so that every pull respects the lifetime of the generic argument mock
        var container = new MockContainer(this, enumerableType, MockLifetime.Transient);

        container.Instantiater.Set(() =>
        {
            if (MockTypeToContainer.TryGetValue(genericArgumentType, out MockContainer? genericArgumentContainer))
            {
                object mockedItem = genericArgumentContainer.Pull();

                Array items = Array.CreateInstance(genericArgumentType, 1);

                items.SetValue(mockedItem, 0);

                return items;
            }

            object? emptyItems = ArrayEmptyMethodInfo
                .MakeGenericMethod(genericArgumentType)
                .Invoke(obj: null, parameters: null);

            if (emptyItems is null)
            {
                throw new UnreachableException($"The method '{nameof(Array.Empty)}' returned null for the type '{genericArgumentType.Name}'.");
            }

            return emptyItems;
        });

        MockTypeToContainer.Add(container.ServiceType, container);
    }

    /// <exception cref="PullValueTypeException"/>
    /// <exception cref="PullNotMockedException"/>
    public object Pull(Type type)
    {
        if (TestAssistant.AssistantConfiguration.IsAutomaticallyMocking && !MockTypeToContainer.ContainsKey(type))
        {
            if (TypeUtilities.IsValueType(type))
            {
                throw new PullValueTypeException(type);
            }

            if (!MockHooksManager.TryToMockFromHooks(this, type))
            {
                if (TypeUtilities.IsEnumerableType(type, out Type? genericArgumentType))
                {
                    MockEnumerable(type, genericArgumentType);
                }
                else
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

        object substitute = Substitute.For(typesToProxy: [substituteType], constructorArguments);

        //the options are enhanced when the substitute is created which is before any of the user configured substitutions
        //are applied so that the user configured substitutions will always take priority over the enhanced options
        if (TestAssistant.AssistantConfiguration.IsEnhancedOptionsMocking && substituteType.IsGenericType)
        {
            Type substituteGenericType = substituteType.GetGenericTypeDefinition();

            if (TryPullOptionsValueObject(typeof(IOptions<>), substituteGenericType, substituteType, out object? optionValue))
            {
                IOptions<object> optionsInstance = (IOptions<object>)substitute;

                optionsInstance.Value.Returns(optionValue);
            }
            else if (TryPullOptionsValueObject(typeof(IOptionsMonitor<>), substituteGenericType, substituteType, out optionValue))
            {
                IOptionsMonitor<object> optionsMonitorInstance = (IOptionsMonitor<object>)substitute;

                optionsMonitorInstance.CurrentValue.Returns(optionValue);
            }
            else if (TryPullOptionsValueObject(typeof(IOptionsSnapshot<>), substituteGenericType, substituteType, out optionValue))
            {
                IOptionsSnapshot<object> optionsSnapshotInstance = (IOptionsSnapshot<object>)substitute;

                optionsSnapshotInstance.Value.Returns(optionValue);
                optionsSnapshotInstance.Get(Arg.Any<string>()).Returns(optionValue);
            }
        }

        return substitute;
    }

    private bool TryPullOptionsValueObject(Type genericOptionsType, Type substituteGenericType, Type substituteType, [NotNullWhen(true)] out object? optionValue)
    {
        if (substituteGenericType == genericOptionsType)
        {
            Type? optionsValueType = substituteType
                .GetGenericArguments()
                .FirstOrDefault();

            if (optionsValueType is null)
            {
                throw new UnreachableException($"The substitute type '{substituteType.Name}' had no generic argument.");
            }

            optionValue = Pull(optionsValueType);

            //set all string properties of the option value object to string.Empty instead of null
            IEnumerable<PropertyInfo> writableStringProperties = optionsValueType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

            foreach (PropertyInfo stringProperty in writableStringProperties)
            {
                object? value = stringProperty.GetValue(optionValue);
                if (value is null)
                {
                    stringProperty?.SetValue(optionValue, string.Empty);
                }
            }

            return true;
        }

        optionValue = null;

        return false;
    }

    public bool ContainsKey(Type key) => MockTypeToContainer.ContainsKey(key);
    public bool TryGetValue(Type key, [MaybeNullWhen(false)] out MockContainer value) => MockTypeToContainer.TryGetValue(key, out value);
    public IEnumerator<KeyValuePair<Type, MockContainer>> GetEnumerator() => MockTypeToContainer.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}