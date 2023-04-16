namespace Lexicom.AspNetCore.Controllers.Contracts.Extensions;
public static class ErrorResponseExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static bool HasCode(this ErrorResponse? response, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(errorCode);

        return response is not null && response.Codes is not null && response.Codes.Contains(errorCode);
    }
}
