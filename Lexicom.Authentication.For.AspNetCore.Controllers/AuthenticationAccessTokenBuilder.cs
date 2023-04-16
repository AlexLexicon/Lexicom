using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Lexicom.Authentication.For.AspNetCore.Controllers.Configurations;
using Lexicom.Jwt.Extensions;
using Lexicom.Jwt.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authentication.For.AspNetCore.Controllers;
public interface IAuthenticationAccessTokenBuilder
{
    IServiceCollection Services { get; }
    /// <exception cref="ArgumentNullException"/>
    IAuthenticationAccessTokenBuilder ConfigureAuthentication(Action<AuthenticationOptions>? configure);
    /// <exception cref="ArgumentNullException"/>
    IAuthenticationAccessTokenBuilder ConfigureJwtBearer(Action<JwtBearerOptions>? configure);
}
public class AuthenticationAccessTokenBuilder : IAuthenticationAccessTokenBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public AuthenticationAccessTokenBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;
    }

    public IServiceCollection Services { get; }
    private Action<AuthenticationOptions>? ConfigureAuthenticationDelegate { get; set; }
    private Action<JwtBearerOptions>? ConfigureJwtBearerDelegate { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public IAuthenticationAccessTokenBuilder ConfigureAuthentication(Action<AuthenticationOptions>? configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        ConfigureAuthenticationDelegate = configure;

        return this;
    }

    /// <exception cref="ArgumentNullException"/>
    public IAuthenticationAccessTokenBuilder ConfigureJwtBearer(Action<JwtBearerOptions>? configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        ConfigureJwtBearerDelegate = configure;

        return this;
    }

    public void Build()
    {
        Services.AddJwtSecretsOptions(JwtOptions.ACCESS_TOKEN_SECTION);

        Services.ConfigureOptions<AuthenticationOptionsConfiguration>();
        Services.ConfigureOptions<JwtBearerOptionsConfiguration>();

        //This exception handler will catch the 
        //'ClaimDoesNotExistException' or 'ClaimNotValidException' exceptions
        //which can potentially occur if a jwt claims are changed but its still valid
        //it will return an 401 unathorized in this case
        Services.AddSingleton<IExceptionHandler, BearerTokenClaimExceptionHandler>();

        AuthenticationBuilder builder;
        if (ConfigureAuthenticationDelegate is not null)
        {
            builder = Services.AddAuthentication(ConfigureAuthenticationDelegate);
        }
        else
        {
            builder = Services.AddAuthentication();
        }

        if (ConfigureJwtBearerDelegate is not null)
        {
            builder.AddJwtBearer(ConfigureJwtBearerDelegate);
        }
        else
        {
            builder.AddJwtBearer();
        }
    }
}
