using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using Lexicom.AspNetCore.Controllers.Amenities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lexicom.AspNetCore.Controllers.Amenities;
[Controller]
public abstract class LexicomController
{
    private ControllerContext? _controllerContext;

    public HttpContext HttpContext => ControllerContext.HttpContext;
    public ClaimsPrincipal User => HttpContext?.User!;

    [ControllerContext]
    /// <exception cref="ArgumentNullException"/>
    public ControllerContext ControllerContext
    {
        get => _controllerContext ??= new ControllerContext();
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            _controllerContext = value;
        }
    }

    [NonAction]
    public virtual IOkResultBuilder Ok()
    {
        return new OkResultBuilder();
    }

    [NonAction]
    public virtual IOkObjectResultBuilder Ok<T>(T value)
    {
        return new OkObjectResultBuilder
        {
            Value = value
        };
    }

    [NonAction]
    public virtual INoContentResultBuilder NoContent()
    {
        return new NoContentResultBuilder();
    }

    [NonAction]
    public virtual IBadRequestResultBuilder BadRequest()
    {
        return new BadRequestResultBuilder();
    }
    /// <exception cref="ArgumentNullException"/>
    [NonAction]
    public virtual IBadRequestObjectResultBuilder BadRequest(string errorKey, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(errorKey);
        ArgumentNullException.ThrowIfNull(errorMessage);

        return new BadRequestObjectResultBuilder()
            .FromKey(errorKey)
            .WithMessage(errorMessage);
    }
    /// <exception cref="ArgumentNullException"/>
    [NonAction]
    public virtual IBadRequestObjectResultBuilder BadRequest(string errorKey, string errorMessage, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(errorKey);
        ArgumentNullException.ThrowIfNull(errorMessage);
        ArgumentNullException.ThrowIfNull(errorCode);

        return BadRequest(errorKey, errorMessage)
            .AddCode(errorCode);
    }

    [NonAction]
    public virtual IConflictResultBuilder Conflict()
    {
        return new ConflictResultBuilder();
    }
    [NonAction]
    /// <exception cref="ArgumentNullException"/>
    public virtual IConflictObjectResultBuilder Conflict(string errorKey, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(errorKey);
        ArgumentNullException.ThrowIfNull(errorMessage);

        return new ConflictObjectResultBuilder()
            .FromKey(errorKey)
            .WithMessage(errorMessage);
    }
    [NonAction]
    /// <exception cref="ArgumentNullException"/>
    public virtual IConflictObjectResultBuilder Conflict(string errorKey, string errorMessage, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(errorMessage);

        return Conflict(errorKey, errorMessage)
            .AddCode(errorCode);
    }

    [NonAction]
    public virtual IForbidResultBuilder Forbid()
    {
        return new ForbidResultBuilder();
    }
    [NonAction]
    /// <exception cref="ArgumentNullException"/>
    public virtual IForbidObjectResultBuilder Forbid(string errorKey, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(errorKey);
        ArgumentNullException.ThrowIfNull(errorMessage);

        return new ForbidObjectResultBuilder()
            .FromKey(errorKey)
            .WithMessage(errorMessage);
    }
    [NonAction]
    /// <exception cref="ArgumentNullException"/>s
    public virtual IForbidObjectResultBuilder Forbid(string errorKey, string errorMessage, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(errorKey);
        ArgumentNullException.ThrowIfNull(errorMessage);

        return Forbid(errorKey, errorMessage)
            .AddCode(errorCode);
    }

    [NonAction]
    public virtual IUnauthorizedResultBuilder Unauthorized()
    {
        return new UnauthorizedResultBuilder();
    }
    [NonAction]
    /// <exception cref="ArgumentNullException"/>
    public virtual IUnauthorizedObjectResultBuilder Unauthorized(string errorKey, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(errorKey);
        ArgumentNullException.ThrowIfNull(errorMessage);

        return new UnauthorizedObjectResultBuilder()
            .FromKey(errorKey)
            .WithMessage(errorMessage);
    }
    [NonAction]
    /// <exception cref="ArgumentNullException"/>
    public virtual IUnauthorizedObjectResultBuilder Unauthorized(string errorKey, string errorMessage, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(errorKey);
        ArgumentNullException.ThrowIfNull(errorMessage);

        return Unauthorized(errorKey, errorMessage)
            .AddCode(errorCode);
    }

    [NonAction]
    public virtual INotFoundResultBuilder NotFound()
    {
        return new NotFoundResultBuilder();
    }
}
