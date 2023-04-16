using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Options;
public class ChangeEmailTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public const string NAME = "ChangeEmailTokenProvider";

    public ChangeEmailTokenProviderOptions()
    {
        Name = NAME;
    }
}
