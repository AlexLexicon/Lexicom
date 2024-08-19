using FluentAssertions;
using Lexicom.Cryptography.Extensions;
using Lexicom.Cryptography.For.Blazor.WebAssembly.Extensions;
using Lexicom.Cryptography.Options;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.UnitTesting.DependencyInjection.As.Blazor.WebAssembly;

namespace Lexicom.Cryptography.For.Blazor.WebAssembly.UnitTests;
public class CryptographyServiceTests
{
    [Fact]
    public async Task Blazor_Mono_Encryption_And_Decryption()
    {
        var uta = new BlazorUnitTestAttendant();

        uta.Configuration.AddInMemoryCollection(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = "MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU=",
        });

        uta.BlazorBuilder.AddCryptography(c =>
        {
            c.AddStringSecretOptions();
        });

        string originalPlainText = "my plain text";

        var cryptographyService = uta.Get<ICryptographyService>();

        string encryptedbase64 = await cryptographyService.EncryptAsync(originalPlainText);

        string plainText = await cryptographyService.DecryptAsync(encryptedbase64);

        encryptedbase64.Should().NotBeNullOrWhiteSpace();
        encryptedbase64.Should().NotBe(originalPlainText);
        encryptedbase64.Should().NotBe(plainText);

        plainText.Should().NotBeNullOrWhiteSpace();
        plainText.Should().Be(originalPlainText);
    }
}