using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface INotFoundResultBuilder : IResultBuilder
{
}
public class NotFoundResultBuilder : NotFoundResult, INotFoundResultBuilder
{
}
