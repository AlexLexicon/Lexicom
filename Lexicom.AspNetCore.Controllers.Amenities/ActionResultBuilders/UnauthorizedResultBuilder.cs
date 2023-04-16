using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IUnauthorizedResultBuilder : IResultBuilder
{
}
public class UnauthorizedResultBuilder : UnauthorizedResult, IUnauthorizedResultBuilder
{
}
