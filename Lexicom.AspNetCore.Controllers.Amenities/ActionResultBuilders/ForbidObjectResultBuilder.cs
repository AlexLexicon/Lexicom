using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IForbidObjectResultBuilder : IObjectResultBuilder
{
}
public class ForbidObjectResultBuilder : ForbidObjectResult, IForbidObjectResultBuilder
{
    public ForbidObjectResultBuilder() : base(value: null)
    {
    }
}
