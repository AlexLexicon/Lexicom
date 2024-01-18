using Lexicom.Swashbuckle.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json;

namespace Lexicom.Swashbuckle.OperationFilters;
public class DefaultExampleFilter : IOperationFilter
{
    private static ParameterInfo? GetBodyParameter(OperationFilterContext context)
    {
        ParameterInfo[] parameters = context.MethodInfo.GetParameters();

        if (parameters.Length == 1)
        {
            return parameters.First();
        }

        foreach (ParameterInfo parameter in parameters)
        {
            Attribute? fromBodyAttribute = parameter.GetCustomAttribute(typeof(FromBodyAttribute));

            if (fromBodyAttribute is not null)
            {
                return parameter;
            }
        }

        return null;
    }

    /// <exception cref="ArgumentNullException"/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(context);

        var exampleAttribute = (SwaggerExampleAttribute?)context.MethodInfo.GetCustomAttribute(typeof(SwaggerExampleAttribute));

        if (exampleAttribute is not null)
        {
            ParameterInfo? bodyParameter = GetBodyParameter(context);

            if (bodyParameter is not null)
            {
                string json = exampleAttribute.Json;

                try
                {
                    var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);

                    operation.RequestBody.Content["application/json"].Example = CreateFromJsonElement(jsonElement);
                }
                catch (Exception e)
                {
                    throw new JsonSwaggerExampleException(json, e);
                }
            }
        }
    }

    #region Swashbuckle.AspNetCore.SwaggerGen.OpenApiAnyFactory.cs
    /*
     * This is a direct copy of the code from
     * https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerGen/SwaggerGenerator/OpenApiAnyFactory.cs
     * as of 2/8/2024
     * 
     * i decided to copy this logic because I want to report the exception (why the json cant be parsed)
     * back to the caller so that it is not cryptic why its not working
     */
    private static IOpenApiAny CreateFromJsonElement(JsonElement jsonElement)
    {
        if (jsonElement.ValueKind is JsonValueKind.Null)
        {
            return new OpenApiNull();
        }

        if (jsonElement.ValueKind is JsonValueKind.True or JsonValueKind.False)
        {
            return new OpenApiBoolean(jsonElement.GetBoolean());
        }

        if (jsonElement.ValueKind is JsonValueKind.Number)
        {
            if (jsonElement.TryGetInt32(out int intValue))
            {
                return new OpenApiInteger(intValue);
            }

            if (jsonElement.TryGetInt64(out long longValue))
            {
                return new OpenApiLong(longValue);
            }

            if (jsonElement.TryGetSingle(out float floatValue) && !float.IsInfinity(floatValue))
            {
                return new OpenApiFloat(floatValue);
            }

            if (jsonElement.TryGetDouble(out double doubleValue))
            {
                return new OpenApiDouble(doubleValue);
            }
        }

        if (jsonElement.ValueKind is JsonValueKind.String)
        {
            return new OpenApiString(jsonElement.ToString());
        }

        if (jsonElement.ValueKind is JsonValueKind.Array)
        {
            return CreateOpenApiArray(jsonElement);
        }

        if (jsonElement.ValueKind is JsonValueKind.Object)
        {
            return CreateOpenApiObject(jsonElement);
        }

        throw new ArgumentException($"Unsupported value kind {jsonElement.ValueKind}");
    }
    private static OpenApiArray CreateOpenApiArray(JsonElement jsonElement)
    {
        var openApiArray = new OpenApiArray();

        foreach (JsonElement item in jsonElement.EnumerateArray())
        {
            openApiArray.Add(CreateFromJsonElement(item));
        }

        return openApiArray;
    }
    private static OpenApiObject CreateOpenApiObject(JsonElement jsonElement)
    {
        var openApiObject = new OpenApiObject();

        foreach (JsonProperty property in jsonElement.EnumerateObject())
        {
            openApiObject.Add(property.Name, CreateFromJsonElement(property.Value));
        }

        return openApiObject;
    }
    #endregion
}
