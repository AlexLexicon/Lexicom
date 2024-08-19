using FluentAssertions;
using Lexicom.Cryptography.Extensions;
using Lexicom.Cryptography.Options;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.UnitTesting;

namespace Lexicom.Cryptography.UnitTests;
public class CryptographyServiceTests
{
    [Fact]
    public async Task Encryption_And_Decryption()
    {
        var uta = new UnitTestAttendant();

        uta.Configuration.AddInMemoryCollection(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = "MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU=",
        });

        uta.AddLexicomCryptography(c =>
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
