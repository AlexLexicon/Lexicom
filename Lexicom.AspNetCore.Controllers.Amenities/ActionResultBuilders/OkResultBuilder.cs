using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IOkResultBuilder : IResultBuilder
{
}
public class OkResultBuilder : OkResult, IOkResultBuilder
{
}
