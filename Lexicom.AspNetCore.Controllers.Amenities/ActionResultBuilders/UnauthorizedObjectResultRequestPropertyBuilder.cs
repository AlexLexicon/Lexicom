using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IUnauthorizedObjectResultRequestPropertyBuilder : IUnauthorizedObjectResultBuilder, IResultRequestPropertyBuilder
{
}
public class UnauthorizedObjectResultRequestPropertyBuilder : UnauthorizedObjectResultBuilder, IUnauthorizedObjectResultRequestPropertyBuilder
{
    public string? ErrorKey { get; set; }
}
