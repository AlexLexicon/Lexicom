using Microsoft.AspNetCore.Authentication;

namespace Lexicom.Authentication;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    //the scheme used in the authorization header for example: Authorization: 'ApiKey' <key>
    public string HeaderScheme { get; set; } = ApiKeyDefaults.AuthenticationScheme;
}
