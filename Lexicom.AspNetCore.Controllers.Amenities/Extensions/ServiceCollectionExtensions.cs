using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Lexicom.AspNetCore.Controllers.Amenities.Filters;
using Lexicom.AspNetCore.Controllers.Amenities.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAspNetCoreControllersErrorResponseActionFilter(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add<ObjectResultToErrorResponseActionFilter>();
        });

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAspNetCoreControllersInvalidModelStateFactory(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = InvalidModelStateResponse.Factory;
        });

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAspNetCoreControllersExceptionHandlingMiddleware(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<ExceptionHandlingMiddleware>();

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection DebugLexicomAspNetCoreControllersExceptionHandlingMiddleware(this IServiceCollection services, Action<Exception> exceptionDelegate)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(exceptionDelegate);

        services.AddSingleton<IExceptionHandler>(sp =>
        {
            return new DebugExceptionHandler(exceptionDelegate);
        });

        return services;
    }

    private class DebugExceptionHandler(Action<Exception> exceptionDelegate) : IExceptionHandler
    {
        public readonly Action<Exception> _exceptionDelegate = exceptionDelegate;

        public ExceptionHandledResult? HandleException(Exception exception)
        {
            _exceptionDelegate.Invoke(exception);

            return null;
        }
    }
}
