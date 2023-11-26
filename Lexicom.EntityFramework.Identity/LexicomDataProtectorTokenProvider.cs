using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text;

namespace Lexicom.EntityFramework.Identity;
/*
 * Due to a bug: https://stackoverflow.com/questions/58973703/system-missingmethodexception-method-not-found-void-microsoft-aspnetcore-iden
 * I have to copy the source code of the 'DataProtectorTokenProvider' class: https://source.dot.net/#Microsoft.AspNetCore.Identity/DataProtectorTokenProvider.cs,b35de4ff6fb1c726
 */
public abstract class LexicomDataProtectorTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser> where TUser : class
{
    private static Encoding DefaultEncoding { get; } = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

    /// <exception cref="ArgumentNullException"/>
    public LexicomDataProtectorTokenProvider(
        IDataProtectionProvider dataProtectionProvider, 
        IOptions<DataProtectionTokenProviderOptions> options)
    {
        ArgumentNullException.ThrowIfNull(dataProtectionProvider);

        Options = options?.Value ?? new DataProtectionTokenProviderOptions();
        Protector = dataProtectionProvider.CreateProtector(Name ?? "DataProtectorTokenProvider");
    }

    public string Name => Options.Name;
    protected IDataProtector Protector { get; private set; }
    protected DataProtectionTokenProviderOptions Options { get; private set; }

    /// <exception cref="ArgumentNullException"/>
    public virtual async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var ms = new MemoryStream();
        string value = await manager.GetUserIdAsync(user);
        using (var writer = new BinaryWriter(ms, DefaultEncoding, leaveOpen: true))
        {
            writer.Write(DateTimeOffset.UtcNow.UtcTicks);
            writer.Write(value);
            writer.Write(purpose ?? "");
            string? text = null;
            if (manager.SupportsUserSecurityStamp)
            {
                text = await manager.GetSecurityStampAsync(user);
            }

            writer.Write(text ?? "");
        }

        return Convert.ToBase64String(Protector.Protect(ms.ToArray()));
    }

    public virtual async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
    {
        _ = 1;
        try
        {
            var stream = new MemoryStream(Protector.Unprotect(Convert.FromBase64String(token)));
            using var reader = new BinaryReader(stream, DefaultEncoding, leaveOpen: true);
            if (new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero) + Options.TokenLifespan < DateTimeOffset.UtcNow)
            {
                return false;
            }

            string userId = reader.ReadString();
            if (userId != await manager.GetUserIdAsync(user))
            {
                return false;
            }

            if (!string.Equals(reader.ReadString(), purpose))
            {
                return false;
            }

            string text = reader.ReadString();
            if (reader.PeekChar() != -1)
            {
                return false;
            }

            if (manager.SupportsUserSecurityStamp)
            {
                string text2 = text;
                return text2 == await manager.GetSecurityStampAsync(user);
            }

            return text == "";
        }
        catch
        {
        }

        return false;
    }

    public virtual Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
        return Task.FromResult(result: false);
    }
}
