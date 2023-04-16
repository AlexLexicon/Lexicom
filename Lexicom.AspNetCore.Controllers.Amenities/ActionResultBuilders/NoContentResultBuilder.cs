using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface INoContentResultBuilder : IResultBuilder
{
}
public class NoContentResultBuilder : NoContentResult, INoContentResultBuilder
{
}
