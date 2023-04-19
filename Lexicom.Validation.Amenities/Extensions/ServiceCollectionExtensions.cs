using FluentValidation;
using FluentValidation.Resources;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Validation.Amenities.Options;
using Lexicom.Validation.Exceptions;
using Lexicom.Validation.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.Amenities.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="LanguageManagerNotDerivedFromLanguageManagerException"/>
    public static IServiceCollection AddLexicomValidationAmenities(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(services);

        if (ValidatorOptions.Global.LanguageManager is not LanguageManager languageManager)
        {
            throw new LanguageManagerNotDerivedFromLanguageManagerException();
        }

        AddLexicomValidationAmenities(services, languageManager, serviceLifetime);

        return services;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomValidationAmenities(this IServiceCollection services, LanguageManager languageManager, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(languageManager);

        services
            .AddOptions<EmailRuleSetOptions>()
            .BindConfiguration();
        services
            .AddOptions<NameRuleSetOptions>()
            .BindConfiguration();
        services
            .AddOptions<PasswordRequirementsRuleSetOptions>()
            .BindConfiguration();

        services.AddLexicomValidation(options =>
        {
            options.AddRuleSets<AssemblyScanMarker>(serviceLifetime);
        });

        languageManager.AddLexicomAmenitiesTranslations();

        return services;
    }
}