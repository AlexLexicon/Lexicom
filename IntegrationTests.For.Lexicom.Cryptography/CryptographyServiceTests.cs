using Lexicom.Cryptography.Exceptions;
using Lexicom.Cryptography.Extensions;
using Lexicom.Cryptography.Options;
using Lexicom.Cryptography.For.Testing.Extensions;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using System.Diagnostics;
using Lexicom.Cryptography;

namespace IntegrationTests.For.Lexicom.Cryptography;

public class CryptographyServiceTests
{
    [Fact]
    public async Task Encryption_And_Decryption()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.Configuration.AddInMemoryCollection(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = "MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU=",
        });

        ita.TestLexicom(l =>
        {
            l.AddCryptography(c =>
            {
                c.AddStringSecretOptions();
            });
        });

        string originalPlainText = "my plain text";

        //act
        var cryptographyService = ita.Make<ICryptographyService>();

        string encryptedbase64 = await cryptographyService.EncryptAsync(originalPlainText);

        string plainText = await cryptographyService.DecryptAsync(encryptedbase64);

        //assert
        Assert.False(string.IsNullOrWhiteSpace(encryptedbase64));
        Assert.NotEqual(originalPlainText, encryptedbase64);
        Assert.NotEqual(plainText, encryptedbase64);

        Assert.False(string.IsNullOrWhiteSpace(plainText));
        Assert.Equal(originalPlainText, plainText);
    }

    public enum TamperedRegion
    {
        InitializationVector,
        Ciphertext,
        AuthenticationTag,
    }
    [Theory]
    [InlineData(TamperedRegion.InitializationVector)]
    [InlineData(TamperedRegion.Ciphertext)]
    [InlineData(TamperedRegion.AuthenticationTag)]
    public async Task Fail_To_Decrypt_After_Tampering_With_Encryption(TamperedRegion tamperedRegion)
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.Configuration.AddInMemoryCollection(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = "MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU=",
        });

        ita.TestLexicom(l =>
        {
            l.AddCryptography(c =>
            {
                c.AddStringSecretOptions();
            });
        });

        string originalPlainText = "my plain text";

        //act
        var cryptographyService = ita.Make<ICryptographyService>();

        string encryptedBase64 = await cryptographyService.EncryptAsync(originalPlainText);

        //the encrypted bytes are composed of the initialization vector, then the ciphertext,
        //then the authentication tag - flip every bit of a byte within the chosen region to simulate tampering
        byte[] tamperedBytes = Convert.FromBase64String(encryptedBase64);

        int tamperByteIndex = tamperedRegion switch
        {
            //the first byte is always within the 16 byte initialization vector
            TamperedRegion.InitializationVector => 0,
            //the byte immediately after the 16 byte initialization vector is always within the ciphertext
            TamperedRegion.Ciphertext => 16,
            //the last byte is always within the authentication tag
            TamperedRegion.AuthenticationTag => tamperedBytes.Length - 1,
            _ => throw new UnreachableException($"The '{nameof(TamperedRegion)}' value '{tamperedRegion}' is not implemented."),
        };

        tamperedBytes[tamperByteIndex] ^= 0xFF;
        string tamperedBase64 = Convert.ToBase64String(tamperedBytes);

        //assert
        Assert.NotEqual(encryptedBase64, tamperedBase64);

        await Assert.ThrowsAsync<EncryptedTextNotValidException>(async () =>
        {
            await cryptographyService.DecryptAsync(tamperedBase64);
        });
    }
}
