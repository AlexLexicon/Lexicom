using Lexicom.Authentication.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Authentication;

public interface IApiKeyAuthenticationBuilder
{
    IServiceCollection Services { get; }
    /// <exception cref="ArgumentNullException"/>
    IApiKeyAuthenticationBuilder ConfigureApiKey(Action<ApiKeyAuthenticationOptions> configure);
    //replaces the configuration backed 'ConfigurationApiKeyValidator'
    //eg to validate api keys stored in a database
    IApiKeyAuthenticationBuilder UseValidator<TApiKeyValidator>() where TApiKeyValidator : class, IApiKeyValidator;
}
public class ApiKeyAuthenticationBuilder : IApiKeyAuthenticationBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public ApiKeyAuthenticationBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
    }

    public IServiceCollection Services { get; }
    private Action<ApiKeyAuthenticationOptions>? ConfigureApiKeyDelegate { get; set; }
    private Type? ValidatorType { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public IApiKeyAuthenticationBuilder ConfigureApiKey(Action<ApiKeyAuthenticationOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        ConfigureApiKeyDelegate = configure;

        return this;
    }

    public IApiKeyAuthenticationBuilder UseValidator<TApiKeyValidator>() where TApiKeyValidator : class, IApiKeyValidator
    {
        ValidatorType = typeof(TApiKeyValidator);

        return this;
    }

    public virtual void Build()
    {
        if (ValidatorType is not null)
        {
            //a custom validator was provided so we use that instead of the
            //configuration backed default
            Services.TryAddScoped(typeof(IApiKeyValidator), ValidatorType);
        }
        else
        {
            Services.AddApiKeyOptions();
            Services.TryAddScoped<IApiKeyValidator, ConfigurationApiKeyValidator>();
        }

        Services.AddLexicomAuthenticationDefaultScheme();

        AuthenticationBuilder builder = Services.AddAuthentication();

        builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyDefaults.AuthenticationScheme, ConfigureApiKeyDelegate);
    }
}
