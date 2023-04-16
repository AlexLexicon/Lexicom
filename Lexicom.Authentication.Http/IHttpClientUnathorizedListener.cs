namespace Lexicom.Authentication.Http;
public interface IHttpClientUnathorizedListener
{
    Task UnathorizedAsync();
}
