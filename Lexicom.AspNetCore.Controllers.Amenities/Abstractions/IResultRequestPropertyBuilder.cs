namespace Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
public interface IResultRequestPropertyBuilder : IObjectResultBuilder
{
    string? ErrorKey { get; set; }
}
