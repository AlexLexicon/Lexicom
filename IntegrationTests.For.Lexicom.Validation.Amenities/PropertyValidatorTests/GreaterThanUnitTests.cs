using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using Lexicom.Validation.Amenities.Extensions;
using IntegrationTests.For.Lexicom.Validation.Amenities.Constructs;
using IntegrationTests.For.Lexicom.Validation.Amenities.Constructs.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.Testing.Extensions;
using Lexicom.Validation;

namespace IntegrationTests.For.Lexicom.Validation.Amenities.PropertyValidatorTests;

public class GreaterThanUnitTests
{
    [Fact]
    public async Task Message_Is_Expected()
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
        var validator = ita.Make<IRuleSetValidator<GreaterThanRuleSet, string?>>();

        await validator.ValidateAsync("4", TestContext.Current.CancellationToken);
        string asyncMessage = validator.ValidationErrors.First();

        validator.Validate("4");
        string syncMessage = validator.ValidationErrors.First();

        //assert
        Assert.Equal("Must be greater than 5.", asyncMessage);
        Assert.Equal("Must be greater than 5.", syncMessage);
    }
}
