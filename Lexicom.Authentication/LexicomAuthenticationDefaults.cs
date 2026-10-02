namespace Lexicom.Authentication;

public static class LexicomAuthenticationDefaults
{
    //the default policy scheme which forwards each request to either
    //the api key or the bearer scheme based on the authorization header
    public const string AUTHENTICATION_SCHEME = "Lexicom";
}
