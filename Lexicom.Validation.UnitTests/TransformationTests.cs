using FluentAssertions;
using Lexicom.UnitTesting;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.UnitTests.ModelsForTests.RuleSets;
using Lexicom.Validation.UnitTests.ModelsForTests.Transformers;
using System.Diagnostics;
using Xunit;

namespace Lexicom.Validation.UnitTests;
public class TransformationTests
{
    [Theory]
    [InlineData("hello", String123abcRuleSet.MESSAGE)]
    [InlineData("1a", "Must be a valid Number.")]
    [InlineData("a", "Must be a valid Number.")]
    [InlineData("1", null)]
    [InlineData("3", null)]
    public async Task Transform_From_String_To_Integer(string input, string? expectedErrorMessage)
    {
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
            options.AddTransformers<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<String123abcRuleSet, string?, TransformerForIntegerGreaterThan1RuleSet, int>>();

        for (int i = 0; i < 3; i++)
        {
            if (i is 0)
            {
                validator.Validate(input);
            }
            else if (i is 1)
            {
                await validator.ValidateAsync(input);
            }
            else if (i is 2)
            {
                validator.Validation.Invoke(input);
            }
            else
            {
                throw new UnreachableException("The index i should not be anything other than 0, 1 or 2.");
            }

            if (expectedErrorMessage is not null)
            {
                validator.ValidationErrors.Should().HaveCount(1);
                validator.ValidationErrors.First().Should().Be(expectedErrorMessage);
            }
            else
            {
                validator.ValidationErrors.Should().HaveCount(0);
            }
        }
    }

    [Theory]
    [InlineData("hello", String123abcRuleSet.MESSAGE)]
    [InlineData("1a", "Must be a valid Number.")]
    [InlineData("a", "Must be a valid Number.")]
    [InlineData("1", "Must be greater than 1.")]
    [InlineData("3", null)]
    public async Task Transform_From_String_To_Integer_With_Mutliple_RuleSetValidators(string input, string? expectedErrorMessage)
    {
        var uta = new UnitTestAttendant();

        uta.AddLexicomValidation(options =>
        {
            options.AddRuleSets<AssemblyScanMarker>();
            options.AddValidators<AssemblyScanMarker>();
            options.AddTransformers<AssemblyScanMarker>();
        });

        var validator = uta.Get<IRuleSetValidator<String123abcRuleSet, string?, TransformerForIntegerGreaterThan1RuleSetAndString123abcRuleSet, int>>();

        for (int i = 0; i < 3; i++)
        {
            if (i is 0)
            {
                validator.Validate(input);
            }
            else if (i is 1)
            {
                await validator.ValidateAsync(input);
            }
            else if (i is 2)
            {
                validator.Validation.Invoke(input);
            }
            else
            {
                throw new UnreachableException("The index i should not be anything other than 0, 1 or 2.");
            }

            if (expectedErrorMessage is not null)
            {
                validator.ValidationErrors.Should().HaveCount(1);
                validator.ValidationErrors.First().Should().Be(expectedErrorMessage);
            }
            else
            {
                validator.ValidationErrors.Should().HaveCount(0);
            }
        }
    }
}
