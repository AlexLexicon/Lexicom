using FluentValidation;
using FluentValidation.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.Extensions;
public static class ValidationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddValidators<TAssemblyScanMarker>(this IValidationServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(builder);

        AssemblyScanner scanResults = AssemblyScanner.FindValidatorsInAssembly(typeof(TAssemblyScanMarker).Assembly);
        foreach (AssemblyScanner.AssemblyScanResult scanResult in scanResults)
        {
            builder.Services.Add(new ServiceDescriptor(scanResult.InterfaceType, scanResult.ValidatorType, serviceLifetime));
        }

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddRuleSets<TAssemblyScanMarker>(this IValidationServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(builder);

        Type[] concreteTypes = typeof(TAssemblyScanMarker).Assembly.DefinedTypes.ToArray();

        var results = new List<AddRuleSetsResult>();
        foreach (Type concreteType in concreteTypes)
        {
            if (!concreteType.IsAbstract && TypeOrBaseTypeIsAbstractRuleSet(concreteType, out Type? abstractRuleSetGenericArgumentType) && abstractRuleSetGenericArgumentType is not null)
            {
                Type[] interfaceTypes = concreteType.GetInterfaces();

                var ruleSetInterfaces = new List<Type>();
                foreach (Type interfaceType in interfaceTypes)
                {
                    if (InterfaceTypeIsIRuleSet(interfaceType, abstractRuleSetGenericArgumentType))
                    {
                        if (!ruleSetInterfaces.Contains(interfaceType))
                        {
                            ruleSetInterfaces.Add(interfaceType);
                        }
                    }
                }

                results.Add(new AddRuleSetsResult(concreteType, ruleSetInterfaces, abstractRuleSetGenericArgumentType));
            }
        }

        if (results.Any())
        {
            foreach (AddRuleSetsResult result in results)
            {
                //add the AbstractRuleSet type
                builder.Services.Add(new ServiceDescriptor(result.ConcreteType, result.ConcreteType, serviceLifetime));

                foreach (var interfaceType in result.InterfaceTypes)
                {
                    //add all IRuleSet interfaces for the found AbstractRuleSet
                    builder.Services.Add(new ServiceDescriptor(interfaceType, sp =>
                    {
                        return sp.GetRequiredService(result.ConcreteType);
                    }, serviceLifetime));
                }
            }
        }

        return builder;

        bool TypeOrBaseTypeIsAbstractRuleSet(Type type, out Type? abstractRuleSetGenericArgumentType)
        {
            ArgumentNullException.ThrowIfNull(type);

            abstractRuleSetGenericArgumentType = null;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AbstractRuleSet<>))
            {
                abstractRuleSetGenericArgumentType = type.GetGenericArguments()[0];

                return true;
            }

            if (type.BaseType is null)
            {
                return false;
            }

            return TypeOrBaseTypeIsAbstractRuleSet(type.BaseType, out abstractRuleSetGenericArgumentType);
        }
        bool InterfaceTypeIsIRuleSet(Type interfaceType, Type abstractGenericType)
        {
            ArgumentNullException.ThrowIfNull(interfaceType);
            ArgumentNullException.ThrowIfNull(abstractGenericType);

            if (interfaceType.IsInterface && interfaceType.IsGenericType)
            {
                Type ruleSetInterfaceType = typeof(IRuleSet<>).MakeGenericType(abstractGenericType);

                if (interfaceType == ruleSetInterfaceType)
                {
                    return true;
                }
            }

            Type[] subInterfaces = interfaceType.GetInterfaces();

            foreach (Type subInterface in subInterfaces)
            {
                return InterfaceTypeIsIRuleSet(subInterface, abstractGenericType);
            }

            return false;
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddLanguageManager<TLanguageManager>(this IValidationServiceBuilder builder) where TLanguageManager : LanguageManager, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddLanguageManager(builder, new TLanguageManager());
    }
    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddLanguageManager(this IValidationServiceBuilder builder, LanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(languageManager);

        builder.LanguageManager = languageManager;

        return builder;
    }

    private class AddRuleSetsResult
    {
        public AddRuleSetsResult(
            Type concreteType,
            List<Type> interfaceTypes,
            Type abstractRuleSetGenericArgumentType)
        {
            ConcreteType = concreteType;
            InterfaceTypes = interfaceTypes;
            AbstractRuleSetGenericArgumentType = abstractRuleSetGenericArgumentType;
        }

        public Type ConcreteType { get; }
        public List<Type> InterfaceTypes { get; }
        public Type AbstractRuleSetGenericArgumentType { get; }
    }
}
