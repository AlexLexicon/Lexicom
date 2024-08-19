using FluentAssertions;
using Lexicom.UnitTesting;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.UnitTests.ModelsForTests.RuleSets;
using Lexicom.Validation.UnitTests.ModelsForTests.Transformers;
using Xunit;

namespace Lexicom.Validation.UnitTests;
public class TransformationTests
{
    [Fact]
    public void Transform_From_String_To_Integer()
    {
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
            options.AddTransformers<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<IntegerGreaterThan5RuleSet, int, string?, TransformerForIntegerGreaterThan5RuleSet>>();

        validator.Validation.Invoke("abc");
        validator.ValidationErrors.Should().HaveCount(1);
        validator.ValidationErrors.First().Should().Be("Must be a valid Number.");

        validator.Validation.Invoke("3");
        validator.ValidationErrors.Should().HaveCount(1);
        validator.ValidationErrors.First().Should().Be("Must be greater than 5.");

        validator.Validation.Invoke("52");
        validator.ValidationErrors.Should().HaveCount(0);
    }

    [Fact]
    public void Transform_From_String_To_Integer_With_Mutliple_RuleSetValidators()
    {
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
            options.AddTransformers<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<IntegerGreaterThan5RuleSet, int, string?, TransformerForIntegerGreaterThan5RuleSetAndStringDigitsRuleSet>>();

        validator.Validation.Invoke("abc");
        validator.ValidationErrors.Should().HaveCount(1);
        validator.ValidationErrors.First().Should().Be("Must contain only digits.");

        validator.Validation.Invoke("3");
        validator.ValidationErrors.Should().HaveCount(1);
        validator.ValidationErrors.First().Should().Be("Must be greater than 5.");

        validator.Validation.Invoke("52");
        validator.ValidationErrors.Should().HaveCount(0);
    }
}
