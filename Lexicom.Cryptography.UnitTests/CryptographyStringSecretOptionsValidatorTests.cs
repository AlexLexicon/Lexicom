using Lexicom.Cryptography.Extensions;
using Lexicom.Cryptography.Options;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.UnitTesting;
using Microsoft.Extensions.Options;

namespace Lexicom.Cryptography.UnitTests;
public class CryptographyStringSecretOptionsValidatorTests
{
    [Theory]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNA==")]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2")]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2")]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2Nzg=")]
    public void SecretKey_String_Size_Throws_When_Not_Correct_Size(string base64StringSecretKey)
    {
        var uta = new UnitTestAttendant();

        uta.Configuration.AddInMemoryCollection(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = base64StringSecretKey,
        });

        uta.AddLexicomCryptography(c =>
        {
            c.AddStringSecretOptions();
        });

        var cryptographyStringSecretOptions = uta.Get<IOptions<CryptographyStringSecretOptions>>();

        var exception = Assert.Throws<OptionsValidationException>(() =>
        {
            _ = cryptographyStringSecretOptions.Value.Base64StringSecretKey;
        });
    }

    [Theory]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU=")]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2Nw==")]
    public void SecretKey_String_Size_Does_Not_Throw_When_Correct_Size(string base64StringSecretKey)
    {
        var uta = new UnitTestAttendant();

        uta.Configuration.AddInMemoryCollection(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = base64StringSecretKey,
        });

        uta.AddLexicomCryptography(c =>
        {
            c.AddStringSecretOptions();
        });

        var cryptographyStringSecretOptions = uta.Get<IOptions<CryptographyStringSecretOptions>>();

        _ = cryptographyStringSecretOptions.Value.Base64StringSecretKey;
    }
}