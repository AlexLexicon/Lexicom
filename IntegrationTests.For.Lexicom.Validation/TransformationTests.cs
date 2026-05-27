using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.Testing.Extensions;
using IntegrationTests.For.Lexicom.Validation.Constructs;
using IntegrationTests.For.Lexicom.Validation.Constructs.RuleSets;
using IntegrationTests.For.Lexicom.Validation.Constructs.Transformers;
using System.Diagnostics;
using Lexicom.Validation;

namespace IntegrationTests.For.Lexicom.Validation;

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
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddValidation(v =>
            {
                v.AddRuleSets<AssemblyScanMarker>();
                v.AddValidators<AssemblyScanMarker>();
                v.AddTransformers<AssemblyScanMarker>();
            });
        });

        //act
        var validator = ita.Make<IRuleSetValidator<String123abcRuleSet, string?, TransformerForIntegerGreaterThan1RuleSet, int>>();

        for (int i = 0; i < 3; i++)
        {
            if (i is 0)
            {
                validator.Validate(input);
            }
            else if (i is 1)
            {
                await validator.ValidateAsync(input, TestContext.Current.CancellationToken);
            }
            else if (i is 2)
            {
                validator.Validation.Invoke(input);
            }
            else
            {
                throw new UnreachableException("The index i should not be anything other than 0, 1 or 2.");
            }

            //assert
            if (expectedErrorMessage is not null)
            {
                Assert.Single(validator.ValidationErrors);
                Assert.Equal(expectedErrorMessage, validator.ValidationErrors.First());
            }
            else
            {
                Assert.Empty(validator.ValidationErrors);
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
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddValidation(v =>
            {
                v.AddRuleSets<AssemblyScanMarker>();
                v.AddValidators<AssemblyScanMarker>();
                v.AddTransformers<AssemblyScanMarker>();
            });
        });

        //act
        var validator = ita.Make<IRuleSetValidator<String123abcRuleSet, string?, TransformerForIntegerGreaterThan1RuleSetAndString123abcRuleSet, int>>();

        for (int i = 0; i < 3; i++)
        {
            if (i is 0)
            {
                validator.Validate(input);
            }
            else if (i is 1)
            {
                await validator.ValidateAsync(input, TestContext.Current.CancellationToken);
            }
            else if (i is 2)
            {
                validator.Validation.Invoke(input);
            }
            else
            {
                throw new UnreachableException("The index i should not be anything other than 0, 1 or 2.");
            }

            //assert
            if (expectedErrorMessage is not null)
            {
                Assert.Single(validator.ValidationErrors);
                Assert.Equal(expectedErrorMessage, validator.ValidationErrors.First());
            }
            else
            {
                Assert.Empty(validator.ValidationErrors);
            }
        }
    }
}
