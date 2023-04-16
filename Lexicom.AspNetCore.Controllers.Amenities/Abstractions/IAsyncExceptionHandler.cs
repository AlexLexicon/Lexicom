namespace Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
public interface IAsyncExceptionHandler
{
    Task<ExceptionHandledResult?> HandleExceptionAsync(Exception exception);
}
