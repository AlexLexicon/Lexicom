using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lexicom.Swashbuckle.OperationFilters;
public class StandardFilter : IOperationFilter
{
    /// <exception cref="ArgumentNullException"/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(context);

        operation.Responses.Clear();
    }
}
