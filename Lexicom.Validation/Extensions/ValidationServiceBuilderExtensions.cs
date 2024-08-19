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

                results.Add(new AddRuleSetsResult
                {
                    ConcreteType = concreteType,
                    InterfaceTypes = ruleSetInterfaces,
                });
            }
        }

        if (results.Count is not 0)
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
    public static IValidationServiceBuilder AddTransformers<TAssemblyScanMarker>(this IValidationServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(builder);

        Type[] concreteTypes = typeof(TAssemblyScanMarker).Assembly.DefinedTypes.ToArray();

        var results = new List<AddTrasnformersResult>();
        foreach (Type concreteType in concreteTypes)
        {
            if (!concreteType.IsAbstract && TypeOrBaseTypeIsAbstractTransformer(concreteType, out Type? abstractTransformerGenericArgumentPropertyType, out Type? abstractTransformerGenericArgumentInPropertyType, out Type? abstractTransformerGenericArgumentValidatorType))
            {
                if (abstractTransformerGenericArgumentPropertyType is not null && abstractTransformerGenericArgumentInPropertyType is not null)
                {
                    Type[] interfaceTypes = concreteType.GetInterfaces();

                    var transformerInterfaces = new List<Type>();
                    foreach (Type interfaceType in interfaceTypes)
                    {
                        if (InterfaceTypeIsIRuleSetTransformer(interfaceType, abstractTransformerGenericArgumentPropertyType, abstractTransformerGenericArgumentInPropertyType, abstractTransformerGenericArgumentValidatorType))
                        {
                            if (!transformerInterfaces.Contains(interfaceType))
                            {
                                transformerInterfaces.Add(interfaceType);
                            }
                        }
                    }

                    results.Add(new AddTrasnformersResult
                    {
                        ConcreteType = concreteType,
                        InterfaceTypes = transformerInterfaces,
                    });
                }
            }
        }

        if (results.Count is not 0)
        {
            foreach (AddTrasnformersResult result in results)
            {
                //add the AbstractTransformer type
                builder.Services.Add(new ServiceDescriptor(result.ConcreteType, result.ConcreteType, serviceLifetime));

                foreach (var interfaceType in result.InterfaceTypes)
                {
                    //add all IAbstractTransformer interfaces for the found AbstractTransformer
                    builder.Services.Add(new ServiceDescriptor(interfaceType, sp =>
                    {
                        return sp.GetRequiredService(result.ConcreteType);
                    }, serviceLifetime));
                }
            }
        }

        return builder;

        bool TypeOrBaseTypeIsAbstractTransformer(Type type, out Type? abstractTransformerGenericArgumentPropertyType, out Type? abstractTransformerGenericArgumentInPropertyType, out Type? abstractTransformerGenericArgumentValidatorType)
        {
            ArgumentNullException.ThrowIfNull(type);

            abstractTransformerGenericArgumentPropertyType = null;
            abstractTransformerGenericArgumentInPropertyType = null;
            abstractTransformerGenericArgumentValidatorType = null;

            if (type.IsGenericType)
            {
                Type typeDifinition = type.GetGenericTypeDefinition();
                if (typeDifinition == typeof(AbstractRuleSetTransformer<,,>))
                {
                    abstractTransformerGenericArgumentPropertyType = type.GetGenericArguments()[0];
                    abstractTransformerGenericArgumentInPropertyType = type.GetGenericArguments()[1];
                    abstractTransformerGenericArgumentValidatorType = type.GetGenericArguments()[2];

                    return true;
                }
                else if (typeDifinition == typeof(AbstractRuleSetTransformer<,>))
                {
                    abstractTransformerGenericArgumentPropertyType = type.GetGenericArguments()[0];
                    abstractTransformerGenericArgumentInPropertyType = type.GetGenericArguments()[1];

                    return true;
                }
            }

            if (type.BaseType is null)
            {
                return false;
            }

            return TypeOrBaseTypeIsAbstractTransformer(type.BaseType, out abstractTransformerGenericArgumentPropertyType, out abstractTransformerGenericArgumentInPropertyType, out abstractTransformerGenericArgumentValidatorType);
        }
        bool InterfaceTypeIsIRuleSetTransformer(Type interfaceType, Type abstractTransformerGenericArgumentPropertyType, Type abstractTransformerGenericArgumentInPropertyType, Type? abstractTransformerGenericArgumentValidatorType)
        {
            ArgumentNullException.ThrowIfNull(interfaceType);
            ArgumentNullException.ThrowIfNull(abstractTransformerGenericArgumentPropertyType);
            ArgumentNullException.ThrowIfNull(abstractTransformerGenericArgumentInPropertyType);

            if (interfaceType.IsInterface && interfaceType.IsGenericType)
            {
                Type transformerInterfaceType;
                if (abstractTransformerGenericArgumentValidatorType is not null)
                {
                    transformerInterfaceType = typeof(IRuleSetTransfromer<,,>).MakeGenericType(abstractTransformerGenericArgumentPropertyType, abstractTransformerGenericArgumentInPropertyType, abstractTransformerGenericArgumentValidatorType);
                }
                else
                {
                    transformerInterfaceType = typeof(IRuleSetTransfromer<,>).MakeGenericType(abstractTransformerGenericArgumentPropertyType, abstractTransformerGenericArgumentInPropertyType);
                }

                if (interfaceType == transformerInterfaceType)
                {
                    return true;
                }
            }

            Type[] subInterfaces = interfaceType.GetInterfaces();

            foreach (Type subInterface in subInterfaces)
            {
                return InterfaceTypeIsIRuleSetTransformer(subInterface, abstractTransformerGenericArgumentPropertyType, abstractTransformerGenericArgumentInPropertyType, abstractTransformerGenericArgumentValidatorType);
            }

            return false;
        }
    }

    public static IValidationServiceBuilder AddRuleSet<TRuleSet, TProperty>(this IValidationServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where TRuleSet : AbstractRuleSet<TProperty>
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.Add(new ServiceDescriptor(typeof(TRuleSet), typeof(TRuleSet), serviceLifetime));
        builder.Services.Add(new ServiceDescriptor(typeof(IRuleSet<TProperty>), sp =>
        {
            return sp.GetRequiredService(typeof(TRuleSet));
        }, serviceLifetime));

        return builder;
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
        public required Type ConcreteType { get; init; }
        public required List<Type> InterfaceTypes { get; init; }
    }

    private class AddTrasnformersResult
    {
        public required Type ConcreteType { get; init; }
        public required List<Type> InterfaceTypes { get; init; }
    }
}
