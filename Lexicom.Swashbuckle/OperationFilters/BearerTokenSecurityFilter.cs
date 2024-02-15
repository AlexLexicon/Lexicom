using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lexicom.Swashbuckle.OperationFilters;
public class BearerTokenSecurityFilter : IOperationFilter
{
    /// <exception cref="ArgumentNullException"/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(context);

        bool methodHasAllowAnonymousAttribute = context.MethodInfo
            .GetCustomAttributes(typeof(AllowAnonymousAttribute), false)
            .Cast<AllowAnonymousAttribute>()
            .FirstOrDefault() is not null;

        if (!methodHasAllowAnonymousAttribute)
        {
            bool hasAuthorizeAttribute = context.MethodInfo
                .GetCustomAttributes(typeof(AuthorizeAttribute), false)
                .Cast<AuthorizeAttribute>()
                .FirstOrDefault() is not null;

            if (!hasAuthorizeAttribute)
            {
                hasAuthorizeAttribute = context.MethodInfo.DeclaringType?
                    .GetCustomAttributes(typeof(AuthorizeAttribute), false)
                    .Cast<AuthorizeAttribute>()
                    .FirstOrDefault() is not null;
            }

            if (hasAuthorizeAttribute)
            {
                var securityRequirement = new OpenApiSecurityRequirement()
                {
                    {
                        new BearerTokenSecurityScheme(),
                        new List<string>()
                    }
                };

                operation.Security =
                [
                    securityRequirement
                ];
            }
        }
    }
}
