using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Authentication.For.AspNetCore.Controllers;

public class AspNetCoreAuthenticationApiKeyBuilder : AuthenticationApiKeyBuilder
{
    public AspNetCoreAuthenticationApiKeyBuilder(IServiceCollection services) : base(services)
    {
    }

    public override void Build()
    {
        base.Build();

        //This exception handler will catch the
        //'ClaimDoesNotExistException' or 'ClaimNotValidException' exceptions
        //which can potentially occur if an api key is not configured with a claim
        //that is used (eg 'GetEmail') it will return a 401 unauthorized in this case
        Services.TryAddEnumerable(ServiceDescriptor.Singleton<IExceptionHandler, AuthenticationClaimExceptionHandler>());
    }
}
