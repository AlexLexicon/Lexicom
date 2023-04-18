using System.Text.Json.Serialization;

namespace Lexicom.AspNetCore.Controllers.Contracts;
public class ErrorResponse
{
    public const string CODE_UNEXPECTED = "error:unexpected";

    public virtual Dictionary<string, List<string>>? Errors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual List<string>? Codes { get; set; }
}
