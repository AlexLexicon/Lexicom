using Lexicom.Swashbuckle.Exceptions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lexicom.Swashbuckle.OperationFilters;
public class DefaultParametersFilter : IOperationFilter
{
    private readonly SwaggerSettings _options;

    /// <exception cref="ArgumentNullException"/>
    public DefaultParametersFilter(SwaggerSettings options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _options = options;
    }

    /// <exception cref="ArgumentNullException"/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(context);

        IList<OpenApiParameter>? parameters = operation.Parameters;

        if (parameters is not null)
        {
            List<SwaggerParameterAttribute> parameterAttributes = context.MethodInfo
                .GetCustomAttributes(typeof(SwaggerParameterAttribute), false)
                .Cast<SwaggerParameterAttribute>()
                .ToList();

            foreach (OpenApiParameter? parameter in parameters)
            {
                string paramName = parameter.Name;

                SwaggerParameterAttribute? parameterAttribute;
                try
                {
                    parameterAttribute = parameterAttributes
                        .Where(pa => pa.ParamName == paramName)
                        .SingleOrDefault();
                }
                catch (InvalidOperationException e)
                {
                    throw new DuplicateSwaggerParametersException(paramName, context.MethodInfo.Name, e);
                }

                string? schemaDefaultString = null;

                if (parameterAttribute is not null)
                {
                    schemaDefaultString = parameterAttribute.DefaultValue?.ToString();
                }
                else
                {
                    //we can use first or default here because dictionaries do not allow duplicates
                    schemaDefaultString = _options.DefaultParameterValues?
                        .Where(dpv => dpv.Key == paramName)
                        .Select(dpv => dpv.Value)
                        .FirstOrDefault()?
                        .ToString();
                }

                if (schemaDefaultString is not null)
                {
                    parameter.Schema.Default = new OpenApiString(schemaDefaultString);
                    parameter.Required = false;
                }
            }
        }
    }
}