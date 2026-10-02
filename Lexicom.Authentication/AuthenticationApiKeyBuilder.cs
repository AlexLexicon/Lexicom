using Lexicom.Authentication.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Authentication;

public interface IAuthenticationApiKeyBuilder
{
    IServiceCollection Services { get; }
    /// <exception cref="ArgumentNullException"/>
    IAuthenticationApiKeyBuilder ConfigureAuthentication(Action<AuthenticationOptions> configure);
    /// <exception cref="ArgumentNullException"/>
    IAuthenticationApiKeyBuilder ConfigureApiKey(Action<ApiKeyAuthenticationOptions> configure);
    //replaces the configuration backed 'ConfigurationApiKeyValidator'
    //eg to validate api keys stored in a database
    IAuthenticationApiKeyBuilder UseValidator<TApiKeyValidator>() where TApiKeyValidator : class, IApiKeyValidator;
}
public class AuthenticationApiKeyBuilder : IAuthenticationApiKeyBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AuthenticationApiKeyBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
    }

    public IServiceCollection Services { get; }
    private Action<AuthenticationOptions>? ConfigureAuthenticationDelegate { get; set; }
    private Action<ApiKeyAuthenticationOptions>? ConfigureApiKeyDelegate { get; set; }
    private Type? ValidatorType { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public IAuthenticationApiKeyBuilder ConfigureAuthentication(Action<AuthenticationOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        ConfigureAuthenticationDelegate = configure;

        return this;
    }

    /// <exception cref="ArgumentNullException"/>
    public IAuthenticationApiKeyBuilder ConfigureApiKey(Action<ApiKeyAuthenticationOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        ConfigureApiKeyDelegate = configure;

        return this;
    }

    public IAuthenticationApiKeyBuilder UseValidator<TApiKeyValidator>() where TApiKeyValidator : class, IApiKeyValidator
    {
        ValidatorType = typeof(TApiKeyValidator);

        return this;
    }

    public virtual void Build()
    {
        //use a custom validator if provided otherwise use the configuration
        if (ValidatorType is not null)
        {
            Services.TryAddScoped(typeof(IApiKeyValidator), ValidatorType);
        }
        else
        {
            Services.AddLexicomAuthenticationApiKeyOptions();
            Services.TryAddScoped<IApiKeyValidator, ConfigurationApiKeyValidator>();
        }

        Services.AddLexicomAuthenticationDefaultScheme();

        AuthenticationBuilder builder;
        if (ConfigureAuthenticationDelegate is not null)
        {
            builder = Services.AddAuthentication(ConfigureAuthenticationDelegate);
        }
        else
        {
            builder = Services.AddAuthentication();
        }

        builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyDefaults.AuthenticationScheme, configureOptions: ConfigureApiKeyDelegate);
    }
}
