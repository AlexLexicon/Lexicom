using Lexicom.Cryptography.Exceptions;
using System.Security.Cryptography;

namespace Lexicom.Cryptography;
/*
 * encryption uses AES-CBC for confidentiality combined with an HMAC-SHA256 authentication
 * tag (encrypt-then-MAC) so that any tampering with the encrypted text is detected before
 * it is decrypted. AES-GCM is intentionally not used because it is not supported on the
 * Blazor WebAssembly platform, which unfortunatly for now I want to support.
 */
public interface ICiphertextAuthenticator
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="EncryptedTextNotValidException"/>
    (int initializationVectorByteCount, int authenticationTagByteCount) GetByteCountsAndValidateComposite(byte[] composite);

    /// <exception cref="ArgumentNullException"/>
    byte[] ComputeAuthenticationTag(byte[] secretKey, byte[] authenticatedData);
}
public class CiphertextAuthenticator : ICiphertextAuthenticator
{
    private const int INITIALIZATION_VECTOR_BYTE_COUNT = 16;
    private const int AUTHENTICATION_TAG_BYTE_COUNT = 32;

    public CiphertextAuthenticator()
    {
        MessageAuthenticationKeyLabel = "Lexicom.Cryptography.MessageAuthenticationKey"u8.ToArray();
    }

    private byte[] MessageAuthenticationKeyLabel { get; }

    public (int initializationVectorByteCount, int authenticationTagByteCount) GetByteCountsAndValidateComposite(byte[] composite)
    {
        ArgumentNullException.ThrowIfNull(composite);

        if (composite.Length < INITIALIZATION_VECTOR_BYTE_COUNT + AUTHENTICATION_TAG_BYTE_COUNT)
        {
            throw new EncryptedTextNotValidException();
        }

        return (INITIALIZATION_VECTOR_BYTE_COUNT, AUTHENTICATION_TAG_BYTE_COUNT);
    }

    public byte[] ComputeAuthenticationTag(byte[] secretKey, byte[] authenticatedData)
    {
        ArgumentNullException.ThrowIfNull(secretKey);
        ArgumentNullException.ThrowIfNull(authenticatedData);

        //derive a dedicated message authentication key from the secret key so that the
        //AES encryption and the HMAC authentication never use the same key bytes
        byte[] messageAuthenticationKey = HMACSHA256.HashData(secretKey, MessageAuthenticationKeyLabel);

        return HMACSHA256.HashData(messageAuthenticationKey, authenticatedData);
    }
}
