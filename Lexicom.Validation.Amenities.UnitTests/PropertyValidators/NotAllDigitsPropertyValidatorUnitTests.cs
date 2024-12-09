using FluentAssertions;
using Lexicom.UnitTesting;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Amenities.UnitTests.ModelsForTests.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.Amenities.UnitTests.PropertyValidators;
public class NotAllDigitsPropertyValidatorUnitTests
{
    [Theory]
    [InlineData("1")]
    [InlineData("123")]
    public async Task Message_Is_Expected(string input)
    {
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddAmenities();
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<NotAllDigitsRuleSet, string?>>();

        await validator.ValidateAsync(input);

        validator.IsValid.Should().BeFalse();
        validator.ValidationErrors.First().Should().Be("Must not contain only digits.");

        validator.Validate(input);

        validator.IsValid.Should().BeFalse();
        validator.ValidationErrors.First().Should().Be("Must not contain only digits.");
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
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddAmenities();
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<NotAllDigitsRuleSet, string?>>();

        await validator.ValidateAsync(input);

        validator.IsValid.Should().BeTrue();
        validator.ValidationErrors.Should().BeEmpty();

        validator.Validate(input);

        validator.IsValid.Should().BeTrue();
        validator.ValidationErrors.Should().BeEmpty();
    }
}