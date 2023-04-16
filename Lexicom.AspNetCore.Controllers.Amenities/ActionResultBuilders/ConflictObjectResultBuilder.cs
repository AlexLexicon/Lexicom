using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IConflictObjectResultBuilder : IObjectResultBuilder
{
}
public class ConflictObjectResultBuilder : ConflictObjectResult, IConflictObjectResultBuilder
{
    public ConflictObjectResultBuilder() : base(error: null)
    {
    }
}
