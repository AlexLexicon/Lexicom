using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IForbidObjectResultRequestPropertyBuilder : IForbidObjectResultBuilder, IResultRequestPropertyBuilder
{
}
public class ForbidObjectResultRequestPropertyBuilder : ForbidObjectResultBuilder, IForbidObjectResultRequestPropertyBuilder
{
    public string? ErrorKey { get; set; }
}
