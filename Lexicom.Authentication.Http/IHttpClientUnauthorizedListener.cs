namespace Lexicom.Authentication.Http;

public interface IHttpClientUnauthorizedListener
{
    Task UnauthorizedAsync();
}
