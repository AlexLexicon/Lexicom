using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Authentication.For.AspNetCore.Controllers;

public class AspNetCoreAuthenticationAccessTokenBuilder : AuthenticationAccessTokenBuilder
{
    public AspNetCoreAuthenticationAccessTokenBuilder(IServiceCollection services) : base(services)
    {
    }

    public override void Build()
    {
        base.Build();

        //This exception handler will catch the 
        //'ClaimDoesNotExistException' or 'ClaimNotValidException' exceptions
        //which can potentially occur if a jwt's claims are changed but it's
        //still valid it will return a 401 unauthorized in this case
        Services.AddSingleton<IExceptionHandler, BearerTokenClaimExceptionHandler>();
    }
}
