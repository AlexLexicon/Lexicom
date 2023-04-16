namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class AspNetCoreControllersAmenitiesServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersAmenitiesServiceBuilder AddErrorResponseActionFilter(this IAspNetCoreControllersAmenitiesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomAspNetCoreControllersErrorResponseActionFilter();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersAmenitiesServiceBuilder AddInvalidModelStateFactory(this IAspNetCoreControllersAmenitiesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomAspNetCoreControllersInvalidModelStateFactory();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersAmenitiesServiceBuilder AddExceptionHandlingMiddleware(this IAspNetCoreControllersAmenitiesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.AddLexicomAspNetCoreControllersExceptionHandlingMiddleware();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersAmenitiesServiceBuilder DebugExceptionHandlingMiddleware(this IAspNetCoreControllersAmenitiesServiceBuilder builder, Action<Exception> exceptionDelegate)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebApplicationBuilder.Services.DebugLexicomAspNetCoreControllersExceptionHandlingMiddleware(exceptionDelegate);

        return builder;
    }
}
