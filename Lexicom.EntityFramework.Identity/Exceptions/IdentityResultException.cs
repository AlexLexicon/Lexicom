using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Exceptions;
public class IdentityResultException : Exception
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

    public IdentityResultException(IdentityResult? identityResult) : base(GetMessage(identityResult))
    {
        IdentityResult = identityResult;
    }

    public IdentityResult? IdentityResult { get; }
}
