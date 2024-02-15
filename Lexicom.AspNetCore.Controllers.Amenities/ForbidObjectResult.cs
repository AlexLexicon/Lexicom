using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities;
public class ForbidObjectResult : ObjectResult
{
    private const int STATUS_CODE = StatusCodes.Status403Forbidden;

    public ForbidObjectResult(object? value) : base(value)
    {
        StatusCode = STATUS_CODE;
    }
}
