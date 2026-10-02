namespace Lexicom.Authentication;

public static class LexicomAuthenticationDefaults
{
    //the default policy scheme which forwards each request to either
    //the api key or the bearer scheme based on the authorization header
#pragma warning disable IDE1006 // Naming Styles -- This is the naming style microsoft uses in this use case
    public const string AuthenticationScheme = "Lexicom";
#pragma warning restore IDE1006 // Naming Styles
}
