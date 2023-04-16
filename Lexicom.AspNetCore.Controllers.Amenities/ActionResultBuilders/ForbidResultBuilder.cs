using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IForbidResultBuilder : IResultBuilder
{
}
public class ForbidResultBuilder : ForbidResult, IForbidResultBuilder
{
}
