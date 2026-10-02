using Lexicom.Scalar.Exceptions;
using Lexicom.Scalar.Options;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using System.Text.Json.Nodes;

namespace Lexicom.Scalar.OperationTransformers;

public class DefaultParameterOperationTransformer : IOpenApiOperationTransformer
{
    private readonly IOptions<ScalarParameterOptions> _scalarParameterOptions;

    /// <exception cref="ArgumentNullException"/>
    public DefaultParameterOperationTransformer(IOptions<ScalarParameterOptions> scalarParameterOptions)
    {
        ArgumentNullException.ThrowIfNull(scalarParameterOptions);

        _scalarParameterOptions = scalarParameterOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DuplicateScalarDefaultParametersException"/>
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(context);

        IList<IOpenApiParameter>? parameters = operation.Parameters;

        if (parameters is not null && parameters.Count > 0)
        {
            List<ScalarDefaultParameterAttribute> defaultParameterAttributes = context.Description.ActionDescriptor.EndpointMetadata
                .OfType<ScalarDefaultParameterAttribute>()
                .ToList();

            foreach (OpenApiParameter parameter in parameters)
            {
                string? paramName = parameter.Name;

                ScalarDefaultParameterAttribute? defaultParameterAttribute;
                try
                {
                    defaultParameterAttribute = defaultParameterAttributes.SingleOrDefault(pa => pa.ParamName == paramName);
                }
                catch (InvalidOperationException e)
                {
                    throw new DuplicateScalarDefaultParametersException(paramName, context.Description.ActionDescriptor.DisplayName, e);
                }

                string? exampleString = null;

                if (defaultParameterAttribute is not null)
                {
                    exampleString = defaultParameterAttribute.DefaultValue?.ToString();
                }
                else
                {
                    //we can use first or default here because dictionaries do not allow duplicates
                    exampleString = _scalarParameterOptions.Value.DefaultParameters?
                        .Where(dpv => dpv.Key == paramName)
                        .Select(dpv => dpv.Value)
                        .FirstOrDefault()?
                        .ToString();
                }

                if (exampleString is not null)
                {
                    parameter.Example = JsonNode.Parse(exampleString);
                    parameter.Required = defaultParameterAttribute?.IsRequired ?? false;
                }
            }
        }

        return Task.CompletedTask;
    }
}