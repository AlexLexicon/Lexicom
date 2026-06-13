using FluentValidation;
using FluentValidation.Resources;
using Lexicom.Validation.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Validation.Extensions;

public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="LanguageManagerNotDerivedFromLanguageManagerException"/>
    public static IServiceCollection AddLexicomValidation(this IServiceCollection services, Action<IValidationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        if (ValidatorOptions.Global.LanguageManager is not LanguageManager languageManager)
        {
            throw new LanguageManagerNotDerivedFromLanguageManagerException();
        }

        var validationBuilder = new ValidationServiceBuilder(services, languageManager);

        validationBuilder.Services.TryAddTransient(typeof(IRuleSetValidator<,>), typeof(RuleSetValidator<,>));
        validationBuilder.Services.TryAddTransient(typeof(IRuleSetValidator<,,,>), typeof(RuleSetValidator<,,,>));

        configure?.Invoke(validationBuilder);

        validationBuilder.LanguageManager.AddLexicomTranslations();

        return services;
    }
}
