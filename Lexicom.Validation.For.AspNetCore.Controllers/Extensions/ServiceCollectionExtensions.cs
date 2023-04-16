using Lexicom.Validation.For.AspNetCore.Controllers.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.For.AspNetCore.Controllers.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomValidationAspNetCoreControllersRequestBodyActionFilter(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add<RequestBodyValidationActionFilter>();
        });

        return services;
    }
}
