using FluentValidation;
using FluentValidation.Resources;
using Lexicom.Validation.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Validation.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomValidation(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        AddLexicomValidation(services, configuration, null);

        return services;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomValidation(this IServiceCollection services, IConfiguration configuration, Action<IValidationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        if (ValidatorOptions.Global.LanguageManager is not LanguageManager languageManager)
        {
            throw new ILanguageManagerNotDerivedFromLanguageManagerException();
        }

        var ValidationBuilder = new ValidationServiceBuilder(services, configuration, languageManager);

        ValidationBuilder.Services.TryAddScoped(typeof(IRuleSetValidator<,>), typeof(RuleSetValidator<,>));
        ValidationBuilder.Services.TryAddScoped(typeof(IRuleSetValidator<,,,>), typeof(RuleSetValidator<,,,>));

        configure?.Invoke(ValidationBuilder);

        ValidationBuilder.LanguageManager.AddLexicomTranslations();

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomValidationValidators<TAssemblyScanMarker>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        ArgumentNullException.ThrowIfNull(services);

        AssemblyScanner scanResults = AssemblyScanner.FindValidatorsInAssembly(typeof(TAssemblyScanMarker).Assembly);
        foreach (AssemblyScanner.AssemblyScanResult scanResult in scanResults)
        {
            services.Add(new ServiceDescriptor(scanResult.InterfaceType, scanResult.ValidatorType, serviceLifetime));
        }

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomValidationRuleSets<TAssemblyScanMarker>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        ArgumentNullException.ThrowIfNull(services);

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

        if (results.Any())
        {
            foreach (AddRuleSetsResult result in results)
            {
                //add the AbstractRuleSet type
                services.Add(new ServiceDescriptor(result.ConcreteType, result.ConcreteType, serviceLifetime));

                foreach (var interfaceType in result.InterfaceTypes)
                {
                    //add all IRuleSet interfaces for the found AbstractRuleSet
                    services.Add(new ServiceDescriptor(interfaceType, sp =>
                    {
                        return sp.GetRequiredService(result.ConcreteType);
                    }, serviceLifetime));
                }
            }
        }

        return services;

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
    private class AddRuleSetsResult
    {
        public required Type ConcreteType { get; init; }
        public required List<Type> InterfaceTypes { get; init; }
        public required Type AbstractRuleSetGenericArgumentType { get; init; }
    }
}
