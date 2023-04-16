using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lexicom.AspNetCore.Controllers.Amenities;
public class ExceptionHandledResult
{
    public ExceptionHandledResult(HttpStatusCode statusCode) : this(statusCode, null)
    {
    }
    public ExceptionHandledResult(
        HttpStatusCode statusCode,
        IActionResult? result)
    {
        StatusCode = statusCode;
        Result = result;
    }

    public HttpStatusCode StatusCode { get; set; }
    public IActionResult? Result { get; set; }
}
