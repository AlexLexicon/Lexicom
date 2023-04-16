namespace Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
public interface IExceptionHandler
{
    ExceptionHandledResult? HandleException(Exception exception);
}
