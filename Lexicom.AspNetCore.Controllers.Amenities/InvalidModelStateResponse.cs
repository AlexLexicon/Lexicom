using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lexicom.AspNetCore.Controllers.Amenities;
public static class InvalidModelStateResponse
{
    private const string MODELSTATE_JSON_KEY_PROPERTY = "$.";
    private const string MODELSTATE_INCLUDING = "including the following:";
    private const string MODELSTATE_REQUESTBODY = "requestBody";
    private const string MODELSTATE_REQUIRED_FIELD_END = "field is required.";
    private const string REQUIRED_MESSAGE = $"The {MODELSTATE_REQUIRED_FIELD_END}";

    /// <exception cref="ArgumentNullException"/>
    public static IActionResult Factory(ActionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        //standardize the regular model state validation
        //to use the ErrorResponse object
        //and standardizes the messaging format
        Dictionary<string, ModelErrorCollection> modelStateErrors = context.ModelState.Where(kvp => kvp.Value is not null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value!.Errors);

        bool isJsonError = false;
        var errors = new Dictionary<string, IEnumerable<string>>();
        foreach (var modelStateError in modelStateErrors)
        {
            foreach (ModelError error in modelStateError.Value)
            {
                string key = modelStateError.Key;
                string message = error.ErrorMessage;

                if (key is "$")
                {
                    if (message.Contains("Path:") &&
                        message.Contains("LineNumber:") &&
                        message.Contains("BytePositionInLine:"))
                    {
                        isJsonError = true;
                    }
                    else
                    {
                        int includingStartIndex = message.IndexOf(MODELSTATE_INCLUDING);
                        if (includingStartIndex >= 0 && error.ErrorMessage.Contains("missing required"))
                        {
                            string fieldsString = message[(includingStartIndex + MODELSTATE_INCLUDING.Length)..];
                            string[] fields = fieldsString.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                            if (fields.Any())
                            {
                                foreach (string field in fields)
                                {
                                    AddMessage(errors, field, REQUIRED_MESSAGE);
                                }
                            }
                        }
                    }
                }
                if (key.StartsWith(MODELSTATE_JSON_KEY_PROPERTY))
                {
                    string cleanKey = key.Replace(MODELSTATE_JSON_KEY_PROPERTY, "");
                    string field = key[MODELSTATE_JSON_KEY_PROPERTY.Length..];

                    AddMessage(errors, cleanKey, "The field is not supported.");
                }
                else if (key is MODELSTATE_REQUESTBODY)
                {
                    if (isJsonError)
                    {
                        AddMessage(errors, key, "The json is malformed or invalid.");
                    }
                }
                else
                {
                    if (message.EndsWith(MODELSTATE_REQUIRED_FIELD_END))
                    {
                        AddMessage(errors, key, REQUIRED_MESSAGE);
                    }
                }
            }
        }

        if (!errors.Any())
        {
            AddMessage(errors, MODELSTATE_REQUESTBODY, "The json body was invalid.");
        }

        return new BadRequestObjectResult(errors);
    }

    private static void AddMessage(Dictionary<string, IEnumerable<string>> errors, string key, string message)
    {
        if (errors.TryGetValue(key, out IEnumerable<string>? value))
        {
            var messages = (List<string>)value;

            if (!messages.Contains(message))
            {
                messages.Add(message);
            }
        }
        else
        {
            errors.Add(key, new List<string>
            {
                message,
            });
        }
    }
}
