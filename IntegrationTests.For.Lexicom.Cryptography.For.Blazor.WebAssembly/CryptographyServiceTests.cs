using Lexicom.Cryptography;
using Lexicom.Cryptography.Extensions;
using Lexicom.Cryptography.For.Blazor.WebAssembly.Extensions;
using Lexicom.Cryptography.Options;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Testing.DependencyInjection;

namespace IntegrationTests.For.Lexicom.Cryptography.For.Blazor.WebAssembly;

public class CryptographyServiceTests
{
    [Fact]
    public async Task Blazor_Mono_Encryption_And_Decryption()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.Configuration.AddInMemoryCollection(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = "MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU=",
        });

        ita.AddLexicomCryptographyForBlazor(c =>
        {
            c.AddStringSecretOptions();
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
}