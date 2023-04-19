using FluentValidation.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation;
public interface IValidationServiceBuilder
{
    IServiceCollection Services { get; }
    LanguageManager LanguageManager { get; set; }
}
public class ValidationServiceBuilder : IValidationServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public ValidationServiceBuilder(
        IServiceCollection services,
        LanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(languageManager);

        Services = services;
        LanguageManager = languageManager;
    }

    public IServiceCollection Services { get; }
    public LanguageManager LanguageManager { get; set; }
}