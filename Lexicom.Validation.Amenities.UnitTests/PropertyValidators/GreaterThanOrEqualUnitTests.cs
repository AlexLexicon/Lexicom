using FluentAssertions;
using Lexicom.UnitTesting;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Amenities.UnitTests.RuleSets;
using Lexicom.Validation.Extensions;
using Xunit;

namespace Lexicom.Validation.Amenities.UnitTests.PropertyValidators;
public class GreaterThanOrEqualUnitTests
{
    [Fact]
    public async Task Has_Error_Message()
    {
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddAmenities();
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<NumberStringRuleSet, string?>>();

        await validator.ValidateAsync("abc");

        validator.ValidationErrors.Should().HaveCount(1);
        validator.ValidationErrors.First().Should().Be("Must contain only digits.");
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    public async Task Has_Error_Message_Greater_Than(string value)
    {
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddAmenities();
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<NumberStringRuleSet, string?>>();

        await validator.ValidateAsync(value);

        validator.ValidationErrors.Should().HaveCount(1);
        validator.ValidationErrors.First().Should().Be("Must be greater than or equal to 3.");
    }

    [Theory]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("1000")]
    public async Task Has_No_Error_Message(string value)
    {
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddAmenities();
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<NumberStringRuleSet, string?>>();

        await validator.ValidateAsync(value);

        validator.ValidationErrors.Should().BeEmpty();
    }
}
