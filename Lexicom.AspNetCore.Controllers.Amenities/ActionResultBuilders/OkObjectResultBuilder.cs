using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IOkObjectResultBuilder : IResultBuilder
{
}
public class OkObjectResultBuilder : OkObjectResult, IOkObjectResultBuilder
{
    public OkObjectResultBuilder() : base(value: null)
    {
    }
}
