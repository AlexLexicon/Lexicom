using Lexicom.Swashbuckle.OperationFilters;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Swashbuckle.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomSwaggerGen(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return AddLexicomSwaggerGen(services, null);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomSwaggerGen(this IServiceCollection services, SwaggerSettings? settings)
    {
        ArgumentNullException.ThrowIfNull(services);

        settings ??= new SwaggerSettings();

        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<StandardFilter>();
            options.OperationFilter<DefaultParametersFilter>(settings);
            options.OperationFilter<DefaultExampleFilter>();
            options.OperationFilter<BearerTokenSecurityFilter>();
            options.AddSecurityDefinition("Bearer", new BearerTokenSecurityScheme());
        });

        return services;
    }
}
