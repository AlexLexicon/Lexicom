namespace Lexicom.Http.Exceptions;
public class HttpResponseJsonIsNullException : Exception
{
    public HttpResponseJsonIsNullException() : base($"The deserialized json from the http response was 'null'.")
    {
    }
}
