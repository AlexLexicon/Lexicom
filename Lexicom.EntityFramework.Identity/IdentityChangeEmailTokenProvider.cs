using Lexicom.EntityFramework.Identity.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace Lexicom.EntityFramework.Identity;
public class IdentityChangeEmailTokenProvider<TUser> : LexicomDataProtectorTokenProvider<TUser> where TUser : class
{
    public IdentityChangeEmailTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<ChangeEmailTokenProviderOptions> options) : base(dataProtectionProvider, options)
    {
    }
}