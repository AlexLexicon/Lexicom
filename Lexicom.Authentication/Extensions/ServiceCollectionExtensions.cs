using Lexicom.Authentication.Configurations;
using Lexicom.Authentication.Options;
using Lexicom.Authentication.Validators;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Options.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Lexicom.Authentication.Extensions;

public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAuthentication(this IServiceCollection services, IConfiguration configuration, Action<IAuthenticationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        configure?.Invoke(new AuthenticationServiceBuilder(services, configuration));

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomApiKeyAuthentication(this IServiceCollection services, Action<IApiKeyAuthenticationBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var apiKeyAuthenticationBuilder = new ApiKeyAuthenticationBuilder(services);

        configure?.Invoke(apiKeyAuthenticationBuilder);

        apiKeyAuthenticationBuilder.Build();

        return services;
    }

    //registers the default policy scheme which forwards to the api key or bearer scheme
    //this is safe to call multiple times
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAuthenticationDefaultScheme(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<AuthenticationOptions>, AuthenticationOptionsConfiguration>());
        services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<PolicySchemeOptions>, PolicySchemeOptionsConfiguration>());

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddApiKeyOptions(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddLexicomValidationAmenities();

        services
            .AddOptions<ApiKeyOptions>()
            .BindConfiguration()
            .Validate<ApiKeyOptions, ApiKeyOptionsValidator>()
            .ValidateOnStart();

        return services;
    }
}
