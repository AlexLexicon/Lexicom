using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Lexicom.AspNetCore.Controllers.Amenities.Middlewares;
public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly List<IAsyncExceptionHandler> _asyncExceptionHandlers;
    private readonly List<IExceptionHandler> _exceptionHandlers;

    /// <exception cref="ArgumentNullException"/>
    public ExceptionHandlingMiddleware(
        ILogger<ExceptionHandlingMiddleware> logger,
        IEnumerable<IAsyncExceptionHandler> asyncExceptionHandlers,
        IEnumerable<IExceptionHandler> exceptionHandlers)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(asyncExceptionHandlers);
        ArgumentNullException.ThrowIfNull(exceptionHandlers);

        _logger = logger;
        _asyncExceptionHandlers = asyncExceptionHandlers.ToList();
        _exceptionHandlers = exceptionHandlers.ToList();
    }

    /// <exception cref="ArgumentNullException"/>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            try
            {
                HttpStatusCode httpStatusCode = default;
                IActionResult? actionResult = null;

                bool isHandled = false;

                foreach (IAsyncExceptionHandler handler in _asyncExceptionHandlers)
                {
                    if (isHandled)
                    {
                        break;
                    }

                    ExceptionHandledResult? handlerResult = await handler.HandleExceptionAsync(exception);

                    if (handlerResult is not null)
                    {
                        isHandled = true;
                        httpStatusCode = handlerResult.StatusCode;
                        actionResult = handlerResult.Result;
                    }
                }

                foreach (IExceptionHandler handler in _exceptionHandlers)
                {
                    if (isHandled)
                    {
                        break;
                    }

                    ExceptionHandledResult? handlerResult = handler.HandleException(exception);

                    if (handlerResult is not null)
                    {
                        isHandled = true;
                        httpStatusCode = handlerResult.StatusCode;
                        actionResult = handlerResult.Result;
                    }
                }

                if (!isHandled)
                {
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    actionResult = new ObjectResult(ControllerErrorResponse.UnexpectedError);

                    //we only log the exception if it is not handled
                    //each IExceptionHandler will have to do its own logging 
                    //if that filter handles the exception

                    if (exception is UnreachableException)
                    {
                        _logger.LogCritical(exception, "An unexpected unreachable exception occured.");
                    }
                    else
                    {
                        _logger.LogError(exception, "An unexpected exception occured.");
                    }
                }

                context.Response.StatusCode = (int)httpStatusCode;

                actionResult ??= new EmptyResult();

                if (actionResult is ObjectResult objectResult)
                {
                    context.Response.ContentType = "application/json";

                    string jsonResult = JsonSerializer.Serialize(objectResult.Value);
                    await context.Response.WriteAsync(jsonResult);
                }
            }
            catch (Exception middlewareException)
            {
                try
                {
                    _logger.LogCritical(middlewareException, "Unexpected error while filtering an API Exception.");

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    string jsonResult = JsonSerializer.Serialize(ControllerErrorResponse.UnexpectedError);
                    await context.Response.WriteAsync(jsonResult);
                }
                catch
                {
                    //we catch here because no matter what we dont want to return any exception after this handler
                }
            }
        }
    }
}