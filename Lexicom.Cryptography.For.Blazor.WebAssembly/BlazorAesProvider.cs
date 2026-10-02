using System.Security.Cryptography;
using Lexicom.Cryptography.For.Blazor.WebAssembly.MonoSecurityCryptography;

namespace Lexicom.Cryptography.For.Blazor.WebAssembly;

public class BlazorAesProvider : IAesProvider
{
    public Aes Create()
    {
        return new MonoAesCryptoServiceProvider();
    }
}
