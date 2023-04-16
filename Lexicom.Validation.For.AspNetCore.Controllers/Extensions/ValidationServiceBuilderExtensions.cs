namespace Lexicom.Validation.For.AspNetCore.Controllers.Extensions;
public static class ValidationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddRequestBodyActionFilter(this IValidationServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomValidationAspNetCoreControllersRequestBodyActionFilter();

        return builder;
    }
}
