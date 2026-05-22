using Lexicom.Testing.DependencyInjection.Exceptions;
using Lexicom.Testing.DependencyInjection.Extensions;
using Lexicom.Testing.DependencyInjection.Mocking;
using Lexicom.Testing.DependencyInjection.Utility;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Lexicom.Testing.DependencyInjection;

public interface ITestAssistant : IDisposable
{
    TestingCategory Category { get; }
    TestAssistantConfiguration AssistantConfiguration { get; }

    T Make<T>(params object[] manualParameters) where T : class;
    object Make(Type type, params object[] manualParameters);
}
public abstract class TestAssistant : ITestAssistant
{
    public TestAssistant(TestAssistantConfiguration configuration)
    {
        AssistantConfiguration = configuration;

        MockManager = new MockManager(this);
    }

    public abstract TestingCategory Category { get; }
    public TestAssistantConfiguration AssistantConfiguration { get; }
    internal MockManager MockManager { get; }

    public virtual void Dispose()
    {
        MockManager.Dispose();
    }

    public virtual UnitTestAssistantMockFluentBuilder<TService> Mock<TService>() where TService : class => MockManager.Mock<TService>();
    public virtual UnitTestAssistantMockFluentBuilder<TService> Mock<TService>(MockLifetime lifetime) where TService : class => MockManager.Mock<TService>(lifetime);

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="MakeZeroConstructorsException"/>
    /// <exception cref="MakeTooManyConstructorsException"/>
    /// <exception cref="PullValueTypeException"/>
    /// <exception cref="PullNotMockedException"/>
    /// <exception cref="MakeUnusedManualParametersException"/>
    public virtual T Make<T>(params object[] manualParameters) where T : class
    {
        return (T)Make(typeof(T), manualParameters);
    }
    public virtual object Make(Type type, params object[] manualParameters)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(manualParameters);

        Type makeType = GetMakeType(type);

        ConstructorInfo[] constructors = makeType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

        if (constructors.Length <= 0)
        {
            throw new MakeZeroConstructorsException(makeType);
        }

        if (constructors.Length > 1)
        {
            throw new MakeTooManyConstructorsException(makeType, constructors.Length);
        }

        bool[] isManualParameterUsed = new bool[manualParameters.Length];

        ConstructorInfo constructor = constructors.Single();
        ParameterInfo[] parameters = constructor.GetParameters();

        object[] resolvedParameters = new object[parameters.Length];
        for (int parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
        {
            bool isResolved = false;
            ParameterInfo parameter = parameters[parameterIndex];
            Type parameterType = parameter.ParameterType;

            for (int manualParameterIndex = 0; manualParameterIndex < manualParameters.Length; manualParameterIndex++)
            {
                if (!isManualParameterUsed[manualParameterIndex])
                {
                    object candidate = manualParameters[manualParameterIndex];

                    if (TypeUtilities.IsAssignableTo(candidate, parameterType))
                    {
                        isManualParameterUsed[manualParameterIndex] = true;

                        resolvedParameters[parameterIndex] = candidate;

                        isResolved = true;

                        break;
                    }
                }
            }

            if (!isResolved)
            {
                ResolveParameter(parameterIndex, resolvedParameters, parameterType);
            }
        }

        var unusedManualParameterTypeNames = new HashSet<string>();
        for (int manualParameterIndex = 0; manualParameterIndex < manualParameters.Length; manualParameterIndex++)
        {
            if (!isManualParameterUsed[manualParameterIndex])
            {
                string typeName = manualParameters[manualParameterIndex]?.GetType().FullName ?? "null";

                unusedManualParameterTypeNames.Add($"[{manualParameterIndex}:{typeName}]");
            }
        }

        if (unusedManualParameterTypeNames.Count > 0)
        {
            throw new MakeUnusedManualParametersException(makeType, unusedManualParameterTypeNames);
        }

        return MakeInstance(type, makeType, constructor, resolvedParameters);
    }

    protected virtual Type GetMakeType(Type type)
    {
        return type;
    }

    protected virtual object MakeInstance(Type type, Type makeType, ConstructorInfo constructor, object[] resolvedParameters)
    {
        return constructor.Invoke(resolvedParameters);
    }

    protected virtual void ResolveParameter(int parameterIndex, object[] resolvedParameters, Type parameterType)
    {
        resolvedParameters[parameterIndex] = PullAndEnhanceInstance(parameterType);
    }

    protected virtual object PullAndEnhanceInstance(Type parameterType)
    {
        object instance = MockManager.Pull(parameterType);

        if (AssistantConfiguration.IsEnhancedOptionsMocking && instance.IsSubstitute() && parameterType.IsGenericType)
        {
            Type substituteGenericType = parameterType.GetGenericTypeDefinition();

            object? optionValue;
            if (TryPullOptionsValueObject(typeof(IOptions<>), substituteGenericType, parameterType, out optionValue))
            {
                IOptions<object> optionsInstance = (IOptions<object>)instance;

                optionsInstance.Value.Returns(optionValue);
            }
            else if (TryPullOptionsValueObject(typeof(IOptionsMonitor<>), substituteGenericType, parameterType, out optionValue))
            {
                IOptionsMonitor<object> optionsMonitorInstance = (IOptionsMonitor<object>)instance;

                optionsMonitorInstance.CurrentValue.Returns(optionValue);
            }
            else if (TryPullOptionsValueObject(typeof(IOptionsSnapshot<>), substituteGenericType, parameterType, out optionValue))
            {
                IOptionsSnapshot<object> optionsSnapshotInstance = (IOptionsSnapshot<object>)instance;

                optionsSnapshotInstance.Value.Returns(optionValue);
                optionsSnapshotInstance.Get(Arg.Any<string>()).Returns(optionValue);
            }
        }

        return instance;
    }

    protected virtual bool TryPullOptionsValueObject(Type genericOptionsType, Type substituteGenericType, Type substituteType, [NotNullWhen(true)] out object? optionValue)
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

            optionValue = MockManager.Pull(optionsValueType);

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
}
