using Lexicom.EntityFramework.Identity.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace Lexicom.EntityFramework.Identity;
public class IdentityEmailConfirmationTokenProvider<TUser> : LexicomDataProtectorTokenProvider<TUser> where TUser : class
{
    public IdentityEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<EmailConfirmationTokenProviderOptions> options) : base(dataProtectionProvider, options)
    {
    }
}
