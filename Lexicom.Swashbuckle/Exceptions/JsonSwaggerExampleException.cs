namespace Lexicom.Swashbuckle.Exceptions;
public class JsonSwaggerExampleException : Exception
{
    public JsonSwaggerExampleException(string? json, Exception? innerException) : base($"There was a problem with the json example '{json ?? "null"}'.", innerException)
    {
    }
}
