using System.Security.Cryptography;

namespace Lexicom.Cryptography;
public interface IAesProvider
{
    Aes Create();
}
public class AesProvider : IAesProvider
{
    public virtual Aes Create()
    {
        return Aes.Create();
    }
}
