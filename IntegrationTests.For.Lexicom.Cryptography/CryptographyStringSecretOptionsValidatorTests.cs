using Lexicom.Cryptography.Extensions;
using Lexicom.Cryptography.Options;
using Lexicom.Cryptography.For.Testing.Extensions;
using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IntegrationTests.For.Lexicom.Cryptography;

public class CryptographyStringSecretOptionsValidatorTests
{
    [Theory]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNA==")]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2Nzg5MTIzNDU2")]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2")]
    [InlineData("MTIzNDU2Nzg5MTIzNDU2Nzg=")]
    public void SecretKey_String_Size_Throws_When_Not_Correct_Size(string base64StringSecretKey)
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.Configuration.AddInMemoryCollection(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = base64StringSecretKey,
        });

        ita.TestLexicom(l =>
        {
            l.AddCryptography(c =>
            {
                c.AddStringSecretOptions();
            });
        });

        //act
        var cryptographyStringSecretOptions = ita.Make<IOptions<CryptographyStringSecretOptions>>();

        //assert
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
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.Configuration.AddInMemoryCollection(new CryptographyStringSecretOptions
        {
            Base64StringSecretKey = base64StringSecretKey,
        });

        ita.TestLexicom(l =>
        {
            l.AddCryptography(c =>
            {
                c.AddStringSecretOptions();
            });
        });

        //act
        var cryptographyStringSecretOptions = ita.Make<IOptions<CryptographyStringSecretOptions>>();

        //assert
        Assert.Equal(base64StringSecretKey, cryptographyStringSecretOptions.Value.Base64StringSecretKey);
    }
}