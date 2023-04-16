using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
public interface IConflictResultBuilder : IResultBuilder
{
}
public class ConflictResultBuilder : ConflictResult, IConflictResultBuilder
{
}
