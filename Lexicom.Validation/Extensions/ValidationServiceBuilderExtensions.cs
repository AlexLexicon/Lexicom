﻿using FluentValidation;
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
                    AbstractRuleSetGenericArgumentType = abstractRuleSetGenericArgumentType,
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
        public required Type AbstractRuleSetGenericArgumentType { get; init; }
    }
}
