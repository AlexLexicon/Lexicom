using Lexicom.Authentication.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddApiKeysOptions(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddOptions<ApiKeyOptions>()
            .BindConfiguration(ApiKeyOptions.SECTION)
            .Validate(options => options.KeyDescriptions.All(key => !string.IsNullOrWhiteSpace(key.Key)), $"Every api key configured in the '{ApiKeyOptions.SECTION}' section must have a non-empty '{nameof(ApiKeyOptionsDescriptor.Key)}'.")
            .ValidateOnStart();

        return services;
    }
}
