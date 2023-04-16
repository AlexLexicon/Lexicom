using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Options;
public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public const string NAME = "EmailConfirmationTokenProvider";

    public EmailConfirmationTokenProviderOptions()
    {
        Name = NAME;
    }
}
