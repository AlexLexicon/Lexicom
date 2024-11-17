using FluentAssertions;
using FluentValidation;
using Lexicom.UnitTesting;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Amenities.PropertyValidators;
using Lexicom.Validation.Amenities.UnitTests.ModelsForTests.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.Amenities.UnitTests.PropertyValidators;
public class DirectoryExistsPropertyValidatorTests
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

        var validator = new DirectoryExistsPropertyValidator<string?>();

        bool result = validator.IsValid(new ValidationContext<string?>(""), "tests");

        //await validator.ValidateAsync("abc");

        //validator.ValidationErrors.Should().HaveCount(1);
        //validator.ValidationErrors.First().Should().Be("Must contain only digits.");
    }
}
