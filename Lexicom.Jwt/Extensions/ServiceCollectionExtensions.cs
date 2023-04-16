using Lexicom.DependencyInjection.Extensions;
using Lexicom.Jwt.Options;
using Lexicom.Jwt.Validators;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Options.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lexicom.Jwt.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddJwtSecretsOptions(this IServiceCollection services, string? nameConfigurationSectionPath = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddLexicomValidationAmenities();

        OptionsBuilder<JwtOptions> builder;
        if (nameConfigurationSectionPath is not null)
        {
            builder = services
                .AddOptions<JwtOptions>(nameConfigurationSectionPath)
                .BindConfiguration(nameConfigurationSectionPath);
        }
        else
        {
            builder = services
                .AddOptions<JwtOptions>()
                .BindConfiguration();
        }

        builder
            .Validate<JwtOptions, JwtOptionsValidator>()
            .ValidateOnStart();

        return services;
    }
}
