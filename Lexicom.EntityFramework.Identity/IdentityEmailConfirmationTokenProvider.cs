using Lexicom.EntityFramework.Identity.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace Lexicom.EntityFramework.Identity;
/// <exception cref="ArgumentNullException"/>
public class IdentityEmailConfirmationTokenProvider<TUser>(IDataProtectionProvider dataProtectionProvider, IOptions<EmailConfirmationTokenProviderOptions> options) : LexicomDataProtectorTokenProvider<TUser>(dataProtectionProvider, options) where TUser : class
{
}
