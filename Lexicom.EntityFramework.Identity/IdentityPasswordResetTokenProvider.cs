using Lexicom.EntityFramework.Identity.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace Lexicom.EntityFramework.Identity;
/// <exception cref="ArgumentNullException"/>
public class IdentityPasswordResetTokenProvider<TUser>(IDataProtectionProvider dataProtectionProvider, IOptions<PasswordResetTokenProviderOptions> options) : LexicomDataProtectorTokenProvider<TUser>(dataProtectionProvider, options) where TUser : class
{
}
