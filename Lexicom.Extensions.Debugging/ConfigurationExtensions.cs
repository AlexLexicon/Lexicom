using Microsoft.Extensions.Configuration;
using System.Text.Json.Nodes;

namespace Lexicom.Extensions.Debugging;
public static class ConfigurationExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static string ToReadableJsonForDebugging(this IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var json = CreateJsonNode(configuration);

        if (json is null)
        {
            //in order to be consistent with 'ServiceCollectionExtensions.ToReadableJsonForDebugging' we return an empty json object if the result is null
            return """
            {

            }
            """;
        }

        return json.ToString();
    }

    private static JsonNode? CreateJsonNode(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var obj = new JsonObject();
        foreach (IConfigurationSection childSection in configuration.GetChildren())
        {
            if (childSection.Path.EndsWith(":0"))
            {
                var array = new JsonArray();
                foreach (IConfigurationSection arrayChildScetion in configuration.GetChildren())
                {
                    JsonNode? subNode = CreateJsonNode(arrayChildScetion);

                    array.Add(subNode);
                }

                return array;
            }
            else
            {
                JsonNode? subNode = CreateJsonNode(childSection);

                obj.Add(childSection.Key, subNode);
            }
        }

        if (obj.Count is 0 && configuration is IConfigurationSection section)
        {
            //booleans
            if (bool.TryParse(section.Value, out bool boolValue))
            {
                return JsonValue.Create(boolValue);
            }
            //decimal numbers
            else if (decimal.TryParse(section.Value, out decimal decimalValue))
            {
                return JsonValue.Create(decimalValue);
            }
            //integer numbers
            else if (long.TryParse(section.Value, out long longValue))
            {
                return JsonValue.Create(longValue);
            }

            return JsonValue.Create(section.Value);
        }

        return obj;
    }
}
