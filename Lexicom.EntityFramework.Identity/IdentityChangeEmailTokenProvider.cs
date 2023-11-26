using Lexicom.EntityFramework.Identity.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace Lexicom.EntityFramework.Identity;
/// <exception cref="ArgumentNullException"/>
public class IdentityChangeEmailTokenProvider<TUser>(IDataProtectionProvider dataProtectionProvider, IOptions<ChangeEmailTokenProviderOptions> options) : LexicomDataProtectorTokenProvider<TUser>(dataProtectionProvider, options) where TUser : class
{
}