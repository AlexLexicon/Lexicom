using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Options;
public class PasswordResetTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public const string NAME = "PasswordResetTokenProvider";

    public PasswordResetTokenProviderOptions()
    {
        Name = NAME;
    }
}
