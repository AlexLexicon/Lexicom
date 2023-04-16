using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IConflictObjectResultRequestPropertyBuilder : IConflictObjectResultBuilder, IResultRequestPropertyBuilder
{
}
public class ConflictObjectResultRequestPropertyBuilder : ConflictObjectResultBuilder, IConflictObjectResultRequestPropertyBuilder
{
    public string? ErrorKey { get; set; }
}
