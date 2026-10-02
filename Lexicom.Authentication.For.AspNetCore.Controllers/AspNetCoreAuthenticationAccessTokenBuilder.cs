using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Authentication.For.AspNetCore.Controllers;

public class AspNetCoreAuthenticationAccessTokenBuilder : AuthenticationAccessTokenBuilder
{
    public AspNetCoreAuthenticationAccessTokenBuilder(IServiceCollection services) : base(services)
    {
    }

    public override void Build()
    {
        base.Build();

        //This exception handler will catch the 'ClaimDoesNotExistException'
        //or 'ClaimNotValidException' exceptions which can potentially
        //occur if a jwt's claims are changed but it's still valid in that
        //case it will return a 401 unauthorized because of this handler
        Services.TryAddEnumerable(ServiceDescriptor.Singleton<IExceptionHandler, AuthenticationClaimExceptionHandler>());
    }
}
