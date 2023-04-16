using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IBadRequestResultBuilder : IResultBuilder
{
}
public class BadRequestResultBuilder : BadRequestResult, IBadRequestResultBuilder
{
}
