using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.AspNetCore.Controllers.Amenities.Abstractions;
using Lexicom.Jwt.Exceptions;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Lexicom.Authentication.For.AspNetCore.Controllers;

public class AuthenticationClaimExceptionHandler : IExceptionHandler
{
    private readonly ILogger<AuthenticationClaimExceptionHandler> _logger;

    /// <exception cref="ArgumentNullException"/>
    public AuthenticationClaimExceptionHandler(ILogger<AuthenticationClaimExceptionHandler> logger)
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
                _logger.LogError(exception, "The '{claim}' claim was not included in the authenticated claims.", claimDoesNotExistException.GetClaimSourceAndNameString());
            }

            return new ExceptionHandledResult(HttpStatusCode.Unauthorized);
        }
        else if (exception is ClaimNotValidException claimNotValidException)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(exception, "The '{claim}' claim was not valid, in many cases this is because the claim is not a valid Guid.", claimNotValidException.GetClaimSourceAndNameString());
            }

            return new ExceptionHandledResult(HttpStatusCode.Unauthorized);
        }

        return null;
    }
}