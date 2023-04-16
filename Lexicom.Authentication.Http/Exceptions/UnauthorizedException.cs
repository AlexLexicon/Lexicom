using System.Net;

namespace Lexicom.Authentication.Http.Exceptions;
public class UnauthorizedException : Exception
{
    public UnauthorizedException(HttpMethod? httpMethod, string? url, HttpStatusCode httpStatusCode) : base($"The http '{httpMethod?.ToString() ?? "null"}' request to '{url ?? "null"}' returned the status code '{httpStatusCode}' unauthorized.")
    {
    }
}
