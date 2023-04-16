using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Extensions;
public static class IdentityResultExtensions
{
    private const string ERROR_DUPLICATE_EMAIL = "DuplicateEmail";
    private const string ERROR_USER_ALREADY_IN_ROLE = "UserAlreadyInRole";
    private const string ERROR_USER_NOT_IN_ROLE = "UserNotInRole";
    private const string ERROR_INVALID_TOKEN = "InvalidToken";
    private const string ERROR_INVALID_ROLENAME = "InvalidRoleName";
    private const string ERROR_INVALID_EMAIL = "InvalidEmail";
    private const string ERROR_PASSWORD_MISMATCH = "PasswordMismatch";
    private const string ERROR_PASSWORD_REQUIRES_TOOSHORT = "PasswordTooShort";
    private const string ERROR_PASSWORD_REQUIRES_NONALPHANUMERIC = "PasswordRequiresNonAlphanumeric";
    private const string ERROR_PASSWORD_REQUIRES_LOWER = "PasswordRequiresLower";
    private const string ERROR_PASSWORD_REQUIRES_UPPER = "PasswordRequiresUpper";
    private const string ERROR_PASSWORD_REQUIRES_DIGIT = "PasswordRequiresDigit";
    private const string ERROR_PASSWORD_REQUIRES_UNIQUECHARS = "PasswordRequiresUniqueChars";

    /// <exception cref="ArgumentNullException"/>
    public static bool HasError(this IdentityResult identityResult, params string[] errorCodes)
    {
        ArgumentNullException.ThrowIfNull(identityResult);
        ArgumentNullException.ThrowIfNull(errorCodes);

        return HasError(identityResult, errorCodes.AsEnumerable());
    }
    /// <exception cref="ArgumentNullException"/>
    public static bool HasError(this IdentityResult identityResult, IEnumerable<string> errorCodes)
    {
        ArgumentNullException.ThrowIfNull(identityResult);
        ArgumentNullException.ThrowIfNull(errorCodes);

        if (identityResult.Errors is null)
        {
            return false;
        }

        return identityResult.Errors.Any(e => e is not null && errorCodes.Contains(e.Code));
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasDuplicateEmailError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_DUPLICATE_EMAIL);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasUserInRoleAlreadyError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_USER_ALREADY_IN_ROLE);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasUserNotInRoleError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_USER_NOT_IN_ROLE);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasInvalidTokenError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_INVALID_TOKEN);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasInvalidRoleNameError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_INVALID_ROLENAME);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasInvalidEmailError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_INVALID_EMAIL);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasPasswordMismatchError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_PASSWORD_MISMATCH);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasPasswordRequiresTooShortError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_PASSWORD_REQUIRES_TOOSHORT);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasPasswordRequiresNonAlphanumericError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_PASSWORD_REQUIRES_NONALPHANUMERIC);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasPasswordRequiresLowerError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_PASSWORD_REQUIRES_LOWER);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasPasswordRequiresUpperError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_PASSWORD_REQUIRES_UPPER);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasPasswordRequiresDigitError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_PASSWORD_REQUIRES_DIGIT);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasPasswordRequiresUniqueCharsError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(ERROR_PASSWORD_REQUIRES_UNIQUECHARS);
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasPasswordMissingRequirementsError(this IdentityResult identityResult)
    {
        ArgumentNullException.ThrowIfNull(identityResult);

        return identityResult.HasError(
            ERROR_PASSWORD_REQUIRES_TOOSHORT,
            ERROR_PASSWORD_REQUIRES_NONALPHANUMERIC,
            ERROR_PASSWORD_REQUIRES_LOWER,
            ERROR_PASSWORD_REQUIRES_UPPER,
            ERROR_PASSWORD_REQUIRES_DIGIT,
            ERROR_PASSWORD_REQUIRES_UNIQUECHARS);
    }
}
