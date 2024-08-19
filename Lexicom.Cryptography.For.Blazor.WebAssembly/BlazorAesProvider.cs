using Lexicom.Cryptography.For.Blazor.WebAssembly.MonoSecurityCryptography;
using System.Security.Cryptography;

namespace Lexicom.Cryptography.For.Blazor.WebAssembly;
public class BlazorAesProvider : IAesProvider
{
    public Aes Create()
    {
        return new MonoAesCryptoServiceProvider();
    }
}
