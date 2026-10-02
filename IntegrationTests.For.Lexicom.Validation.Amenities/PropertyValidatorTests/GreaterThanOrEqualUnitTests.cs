using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using Lexicom.Validation.Amenities.Extensions;
using IntegrationTests.For.Lexicom.Validation.Amenities.Constructs;
using IntegrationTests.For.Lexicom.Validation.Amenities.Constructs.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.Testing.Extensions;
using Lexicom.Validation;

namespace IntegrationTests.For.Lexicom.Validation.Amenities.PropertyValidatorTests;

public class GreaterThanOrEqualUnitTests
{
    [Fact]
    public async Task Has_Error_Message()
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
        var validator = ita.Make<IRuleSetValidator<NumberStringRuleSet, string?>>();

        await validator.ValidateAsync("abc", TestContext.Current.CancellationToken);

        //assert
        Assert.Single(validator.ValidationErrors);
        Assert.Equal("Must contain only digits.", validator.ValidationErrors.First());
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    public async Task Has_Error_Message_Greater_Than(string value)
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
        var validator = ita.Make<IRuleSetValidator<NumberStringRuleSet, string?>>();

        await validator.ValidateAsync(value, TestContext.Current.CancellationToken);

        //assert
        Assert.Single(validator.ValidationErrors);
        Assert.Equal("Must be greater than or equal to 3.", validator.ValidationErrors.First());
    }

    [Theory]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("1000")]
    public async Task Has_No_Error_Message(string value)
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
        var validator = ita.Make<IRuleSetValidator<NumberStringRuleSet, string?>>();

        await validator.ValidateAsync(value, TestContext.Current.CancellationToken);

        //assert
        Assert.Empty(validator.ValidationErrors);
    }
}
