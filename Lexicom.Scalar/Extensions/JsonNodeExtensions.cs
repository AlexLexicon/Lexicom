using Microsoft.OpenApi.Any;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Lexicom.Scalar.Extensions;

public static class JsonNodeExtensions
{
    //Microsoft.OpenApi 1.x (used by .net 9) represents examples as 'IOpenApiAny'
    //rather than 'JsonNode' so the parsed json has to be converted
    public static IOpenApiAny ToOpenApiAny(this JsonNode? jsonNode)
    {
        if (jsonNode is null)
        {
            return new OpenApiNull();
        }

        if (jsonNode is JsonObject jsonObject)
        {
            var openApiObject = new OpenApiObject();

            foreach ((string propertyName, JsonNode? propertyValue) in jsonObject)
            {
                openApiObject[propertyName] = propertyValue.ToOpenApiAny();
            }

            return openApiObject;
        }

        if (jsonNode is JsonArray jsonArray)
        {
            var openApiArray = new OpenApiArray();

            foreach (JsonNode? item in jsonArray)
            {
                openApiArray.Add(item.ToOpenApiAny());
            }

            return openApiArray;
        }

        JsonValue jsonValue = jsonNode.AsValue();

        switch (jsonValue.GetValueKind())
        {
            case JsonValueKind.String:
                return new OpenApiString(jsonValue.GetValue<string>());
            case JsonValueKind.True:
            case JsonValueKind.False:
                return new OpenApiBoolean(jsonValue.GetValue<bool>());
            case JsonValueKind.Number:
                if (jsonValue.TryGetValue(out int intValue))
                {
                    return new OpenApiInteger(intValue);
                }

                if (jsonValue.TryGetValue(out long longValue))
                {
                    return new OpenApiLong(longValue);
                }

                return new OpenApiDouble(jsonValue.GetValue<double>());
            default:
                return new OpenApiNull();
        }
    }
}
