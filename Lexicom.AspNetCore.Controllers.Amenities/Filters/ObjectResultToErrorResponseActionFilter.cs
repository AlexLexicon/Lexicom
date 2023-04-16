using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Lexicom.AspNetCore.Controllers.Amenities.Filters;
public class ObjectResultToErrorResponseActionFilter : IAsyncActionFilter
{
    private readonly ILogger<ObjectResultToErrorResponseActionFilter> _logger;

    /// <exception cref="ArgumentNullException"/>
    public ObjectResultToErrorResponseActionFilter(ILogger<ObjectResultToErrorResponseActionFilter> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }

    /// <exception cref="ArgumentNullException"/>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        await next();

        if (context.Result is not OkObjectResult and ObjectResult result && result.Value is not ControllerErrorResponse)
        {
            //for any non-OkObjectResult ObjectResult we only allow manually providing Dictionary<string, IEnumerable<string>> or ErrorResponse. Anything else is not allowed
            if (result.Value is Dictionary<string, IEnumerable<string>> errors)
            {
                var errorResponse = new ControllerErrorResponse();

                foreach (KeyValuePair<string, IEnumerable<string>> error in errors)
                {
                    foreach (string errorMessage in error.Value)
                    {
                        errorResponse.AddError(error.Key, errorMessage);
                    }
                }

                result.Value = errorResponse;
            }
            else
            {
                result.Value = ControllerErrorResponse.UnexpectedError;

                //we dont want this else to ever be used so I log critical to hopefully expose this being used
                _logger.LogCritical("A non OkObjectResult ObjectResult.Value of the type '{objectResultValueType}' was returned from an action but only the types '{manualResultType}' or '{errorResponseType}' is allowed", result.Value?.GetType(), typeof(Dictionary<string, IEnumerable<string>>), typeof(ControllerErrorResponse));
            }
        }
    }
}
