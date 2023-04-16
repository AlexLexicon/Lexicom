using FluentValidation.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation;
public interface IValidationServiceBuilder
{
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
    LanguageManager LanguageManager { get; set; }
}
public class ValidationServiceBuilder : IValidationServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public ValidationServiceBuilder(
        IServiceCollection services,
        IConfiguration configuration,
        LanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(languageManager);

        Services = services;
        Configuration = configuration;
        LanguageManager = languageManager;
    }

    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }
    public LanguageManager LanguageManager { get; set; }
}