using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace Lexicom.Scalar.OperationTransformers;

public class BearerSecuritySchemeOperationTransformer : IOpenApiOperationTransformer
{
    /// <exception cref="ArgumentNullException"/>
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(context);

        bool hasControllerAuthorize = false;
        if (context.Description.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        {
            hasControllerAuthorize = controllerActionDescriptor.ControllerTypeInfo
                .GetCustomAttributes(inherit: true)
                .OfType<IAuthorizeData>()
                .Any();
        }

        bool isAuthorized = hasControllerAuthorize;
        if (isAuthorized)
        {
            //if it has a controller level authorization then we need to make sure this endpoint does not allow anonymous

            bool isAllowAnonymous = context.Description.ActionDescriptor.EndpointMetadata
                .OfType<IAllowAnonymous>()
                .Any();

            isAuthorized = !isAllowAnonymous;
        }
        else
        {
            //if it does not have controller level authorization then we need to check if this endpoint is authorized

            isAuthorized = context.Description.ActionDescriptor.EndpointMetadata
                .OfType<IAuthorizeData>()
                .Any();
        }

        if (isAuthorized)
        {
            operation.Security ??= [];
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(BearerTokenDefaults.AuthenticationScheme)] = [],
            });
        }
        else
        {
            //this currently does not work because of the bug outlined in this github issue: https://github.com/scalar/scalar/issues/7400
            operation.Security = [];
        }

        return Task.CompletedTask;
    }
}
