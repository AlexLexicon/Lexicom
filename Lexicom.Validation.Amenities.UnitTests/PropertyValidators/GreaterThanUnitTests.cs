using FluentAssertions;
using Lexicom.UnitTesting;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Amenities.UnitTests.RuleSets;
using Lexicom.Validation.Extensions;
using Xunit;

namespace Lexicom.Validation.Amenities.UnitTests.PropertyValidators;
public class GreaterThanUnitTests
{
    [Fact]
    public async Task Message_Is_Expected()
    {
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddAmenities();
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<GreaterThanRuleSet, string?>>();

        await validator.ValidateAsync("4");

        validator.ValidationErrors.First().Should().Be("Must be greater than 5.");

        validator.Validate("4");
        
        validator.ValidationErrors.First().Should().Be("Must be greater than 5.");
    }
}
