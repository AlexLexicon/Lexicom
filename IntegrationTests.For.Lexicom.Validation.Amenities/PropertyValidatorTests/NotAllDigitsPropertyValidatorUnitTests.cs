using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using Lexicom.Validation.Amenities.Extensions;
using IntegrationTests.For.Lexicom.Validation.Amenities.Constructs;
using IntegrationTests.For.Lexicom.Validation.Amenities.Constructs.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.Testing.Extensions;
using Lexicom.Validation;

namespace IntegrationTests.For.Lexicom.Validation.Amenities.PropertyValidatorTests;

public class NotAllDigitsPropertyValidatorUnitTests
{
    [Theory]
    [InlineData("1")]
    [InlineData("123")]
    public async Task Message_Is_Expected(string input)
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddValidation(v =>
            {
                v.AddAmenities();
                v.AddRuleSets<AssemblyScanMarker>();
                v.AddValidators<AssemblyScanMarker>();
            });
        });

        //act
        var validator = ita.Make<IRuleSetValidator<NotAllDigitsRuleSet, string?>>();

        await validator.ValidateAsync(input, TestContext.Current.CancellationToken);
        bool asyncIsValid = validator.IsValid;
        string asyncMessage = validator.ValidationErrors.First();

        validator.Validate(input);
        bool syncIsValid = validator.IsValid;
        string syncMessage = validator.ValidationErrors.First();

        //assert
        Assert.False(asyncIsValid);
        Assert.Equal("Must not contain only digits.", asyncMessage);

        Assert.False(syncIsValid);
        Assert.Equal("Must not contain only digits.", syncMessage);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("a")]
    [InlineData("abc")]
    [InlineData("a1")]
    [InlineData("abc123")]
    [InlineData("1 2 3")]
    public async Task Message_Is_Not_Expected(string? input)
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddValidation(v =>
            {
                v.AddAmenities();
                v.AddRuleSets<AssemblyScanMarker>();
                v.AddValidators<AssemblyScanMarker>();
            });
        });

        //act
        var validator = ita.Make<IRuleSetValidator<NotAllDigitsRuleSet, string?>>();

        await validator.ValidateAsync(input, TestContext.Current.CancellationToken);
        bool asyncIsValid = validator.IsValid;
        string[] asyncMessages = validator.ValidationErrors.ToArray();

        validator.Validate(input);
        bool syncIsValid = validator.IsValid;
        string[] syncMessages = validator.ValidationErrors.ToArray();

        //assert
        Assert.True(asyncIsValid);
        Assert.Empty(asyncMessages);

        Assert.True(syncIsValid);
        Assert.Empty(syncMessages);
    }
}