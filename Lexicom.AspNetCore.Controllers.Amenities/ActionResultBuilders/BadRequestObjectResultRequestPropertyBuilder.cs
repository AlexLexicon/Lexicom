using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IBadRequestObjectResultRequestPropertyBuilder : IBadRequestObjectResultBuilder, IResultRequestPropertyBuilder
{
}
public class BadRequestObjectResultRequestPropertyBuilder : BadRequestObjectResultBuilder, IBadRequestObjectResultRequestPropertyBuilder
{
    public string? ErrorKey { get; set; }
}
