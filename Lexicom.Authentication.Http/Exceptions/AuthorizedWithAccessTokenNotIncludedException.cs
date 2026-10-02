namespace Lexicom.Authentication.Http.Exceptions;

public class AuthorizedWithAccessTokenNotIncludedException() : Exception($"{nameof(AuthenticationHttpClientBuilder.AutomaticallyRefreshAccessToken)}() requires {nameof(AuthenticationHttpClientBuilder.AuthorizeWithAccessToken)}() to be called first.")
{
}
