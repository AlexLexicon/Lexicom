using System.Net;

namespace Lexicom.Authentication.Http.Exceptions;
public class UnauthorizedException(HttpMethod? httpMethod, string? url, HttpStatusCode httpStatusCode) : Exception($"The http '{httpMethod?.ToString() ?? "null"}' request to '{url ?? "null"}' returned the status code '{httpStatusCode}' unauthorized.")
{
}
