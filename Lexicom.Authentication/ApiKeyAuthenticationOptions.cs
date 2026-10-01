using Microsoft.AspNetCore.Authentication;

namespace Lexicom.Authentication;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    //the scheme used in the authorization header
    //eg 'ApiKey' in the header 'Authorization: ApiKey <key>'
    public string HeaderScheme { get; set; } = ApiKeyDefaults.AuthenticationScheme;
}
