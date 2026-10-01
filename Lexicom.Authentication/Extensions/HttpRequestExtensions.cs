using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Lexicom.Authentication.Extensions;

public static class HttpRequestExtensions
{
    //returns true when the authorization header uses the provided scheme
    //eg 'Authorization: ApiKey <key>' even if the api key itself is missing
    /// <exception cref="ArgumentNullException"/>
    public static bool TryGetApiKey(this HttpRequest request, string headerScheme, out string? apiKey)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(headerScheme);

        apiKey = null;

        string authorizationHeader = request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            return false;
        }

        if (!AuthenticationHeaderValue.TryParse(authorizationHeader, out AuthenticationHeaderValue? authenticationHeaderValue))
        {
            return false;
        }

        if (!string.Equals(authenticationHeaderValue.Scheme, headerScheme, StringComparison.OrdinalIgnoreCase))
        {
            //a different authorization scheme was used (eg 'Bearer')
            return false;
        }

        apiKey = authenticationHeaderValue.Parameter;

        return true;
    }
}
