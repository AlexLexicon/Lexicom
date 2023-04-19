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

        var ValidationBuilder = new ValidationServiceBuilder(services, languageManager);

        ValidationBuilder.Services.TryAddScoped(typeof(IRuleSetValidator<,>), typeof(RuleSetValidator<,>));
        ValidationBuilder.Services.TryAddScoped(typeof(IRuleSetValidator<,,,>), typeof(RuleSetValidator<,,,>));

        configure?.Invoke(ValidationBuilder);

        ValidationBuilder.LanguageManager.AddLexicomTranslations();

        return services;
    }
}
