using Lexicom.Scalar.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json.Nodes;

namespace Lexicom.Scalar.OperationTransformers;

public class DefaultRequestBodyOperationTransformer : IOpenApiOperationTransformer
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="DefaultRequestBodyJsonException"/>
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(context);

        ScalarDefaultRequestBodyAttribute? defaultRequestBodyAttribute = context.Description.ActionDescriptor.EndpointMetadata
            .OfType<ScalarDefaultRequestBodyAttribute>()
            .FirstOrDefault();

        if (defaultRequestBodyAttribute is not null && context.Description.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        {
            ParameterInfo? bodyParameter = GetBodyParameter(controllerActionDescriptor.MethodInfo);

            if (bodyParameter is not null && operation.RequestBody is not null && operation.RequestBody.Content is not null)
            {
                string json = defaultRequestBodyAttribute.Json;

                try
                {
                    operation.RequestBody.Content[MediaTypeNames.Application.Json].Example = JsonNode.Parse(json);
                }
                catch (Exception e)
                {
                    throw new DefaultRequestBodyJsonException(json, e);
                }
            }
        }

        return Task.CompletedTask;
    }

    private static ParameterInfo? GetBodyParameter(MethodInfo actionMethod)
    {
        ParameterInfo[] parameters = actionMethod.GetParameters();

        if (parameters.Length == 1)
        {
            return parameters.First();
        }

        foreach (ParameterInfo parameter in parameters)
        {
            Attribute? fromBodyAttribute = parameter.GetCustomAttribute<FromBodyAttribute>();

            if (fromBodyAttribute is not null)
            {
                return parameter;
            }
        }

        return null;
    }
}
