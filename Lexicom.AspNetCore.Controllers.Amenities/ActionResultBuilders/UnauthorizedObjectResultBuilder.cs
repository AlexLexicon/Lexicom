using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IUnauthorizedObjectResultBuilder : IObjectResultBuilder
{
}
public class UnauthorizedObjectResultBuilder : UnauthorizedObjectResult, IUnauthorizedObjectResultBuilder
{
    public UnauthorizedObjectResultBuilder() : base(value: null)
    {
    }
}
