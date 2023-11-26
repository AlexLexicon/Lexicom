using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lexicom.AspNetCore.Controllers.Amenities;
public class ExceptionHandledResult(HttpStatusCode statusCode, IActionResult? result)
{
    public ExceptionHandledResult(HttpStatusCode statusCode) : this(statusCode, null)
    {
    }

    public HttpStatusCode StatusCode { get; set; } = statusCode;
    public IActionResult? Result { get; set; } = result;
}
