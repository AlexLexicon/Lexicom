using Lexicom.EntityFramework.Identity.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace Lexicom.EntityFramework.Identity;
public class IdentityPasswordResetTokenProvider<TUser> : LexicomDataProtectorTokenProvider<TUser> where TUser : class
{
    public IdentityPasswordResetTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<PasswordResetTokenProviderOptions> options) : base(dataProtectionProvider, options)
    {
    }
}
