namespace Lexicom.Swashbuckle.Exceptions;
public class JsonSwaggerExampleException(string? json, Exception? innerException) : Exception($"There was a problem with the json example '{json ?? "null"}'.", innerException)
{
}
