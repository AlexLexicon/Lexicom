using Lexicom.Scalar.DocumentTransformers;
using Lexicom.Scalar.OperationTransformers;
using Lexicom.Scalar.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Scalar.Extensions;

public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void AddLexicomScalar(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        AddLexicomScalar(services, defaultParameters: null);
    }
    /// <exception cref="ArgumentNullException"/>
    public static void AddLexicomScalar(this IServiceCollection services, Dictionary<string, object?>? defaultParameters)
    {
        ArgumentNullException.ThrowIfNull(services);

        services
            .AddOptions<ScalarParameterOptions>()
            .BindConfiguration(nameof(ScalarParameterOptions))
            .PostConfigure(options =>
            {
                if (defaultParameters is not null)
                {
                    options.DefaultParameters = defaultParameters;
                }
            });

        services.AddOpenApi(options =>
        {
            options.AddOperationTransformer<DefaultRequestBodyOperationTransformer>();
            options.AddOperationTransformer<DefaultParameterOperationTransformer>();
            options.AddDocumentTransformer<BearerSecuritySchemeDocumentTransformer>();
            options.AddOperationTransformer<BearerSecuritySchemeOperationTransformer>();
        });
    }
}
