namespace Lexicom.AspNetCore.Controllers.Contracts;
public class ErrorResponse
{
    public const string CODE_UNEXPECTED = "error:unexpected";

    public virtual Dictionary<string, List<string>>? Errors { get; set; }
    //todo: not include in json when null (or empty?)
    public virtual List<string>? Codes { get; set; }
}
