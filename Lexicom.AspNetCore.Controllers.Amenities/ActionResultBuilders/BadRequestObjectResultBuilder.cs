using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IBadRequestObjectResultBuilder : IObjectResultBuilder
{
}
public class BadRequestObjectResultBuilder : BadRequestObjectResult, IBadRequestObjectResultBuilder
{
    public BadRequestObjectResultBuilder() : base(error: null)
    {
    }
}
