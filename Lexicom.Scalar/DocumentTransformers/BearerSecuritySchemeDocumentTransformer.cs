using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Lexicom.Scalar.DocumentTransformers;

public class BearerSecuritySchemeDocumentTransformer : IOpenApiDocumentTransformer
{
    /// <exception cref="ArgumentNullException"/>
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(context);

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

        document.Components.SecuritySchemes[BearerTokenDefaults.AuthenticationScheme] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
        };
    }
}
