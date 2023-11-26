using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Exceptions;
public class IdentityResultException(IdentityResult? identityResult) : Exception(GetMessage(identityResult))
{
    private static string GetMessage(IdentityResult? identityResult)
    {
        string errorsString;
        if (identityResult is not null)
        {
            errorsString = new IdentityResultErrorsCollection(identityResult.Errors).ToString();
        }
        else
        {
            errorsString = string.Empty;
        }

        return $"identity result errors: {errorsString}";
    }

    public IdentityResult? IdentityResult { get; } = identityResult;
}
