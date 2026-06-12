using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Lexicom.Jwt.Exceptions;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Lexicom.Authentication.For.AspNetCore.Controllers;

public class BearerTokenClaimExceptionHandler : IExceptionHandler
{
    private readonly ILogger<BearerTokenClaimExceptionHandler> _logger;

    /// <exception cref="ArgumentNullException"/>
    public BearerTokenClaimExceptionHandler(ILogger<BearerTokenClaimExceptionHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }

    /// <exception cref="ArgumentNullException"/>
    public ExceptionHandledResult? HandleException(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (exception is ClaimDoesNotExistException claimDoesNotExistException)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(exception, "The '{claimSourceName}:{claim}' claim was not included in the bearer token.", claimDoesNotExistException.ClaimSourceName, claimDoesNotExistException.Claim);
            }

            return new ExceptionHandledResult(HttpStatusCode.Unauthorized);
        }
        else if (exception is ClaimNotValidException claimNotValidException)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(exception, "The '{claimSourceName}:{claim}' claim was not valid, in many cases because the claim is not a valid Guid.", claimNotValidException.ClaimSourceName, claimNotValidException.Claim);
            }

            return new ExceptionHandledResult(HttpStatusCode.Unauthorized);
        }

        return null;
    }
}