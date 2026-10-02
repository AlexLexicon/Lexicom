using FluentValidation.Results;

namespace Lexicom.Validation;

public interface IRuleSetTransformer<TProperty, TNextProperty>
{
    string ErrorMessageTypeName { get; }

    bool TryTransform(TProperty property, out TNextProperty nextProperty);
}
public abstract class AbstractRuleSetTransformer<TProperty, TNextProperty> : IRuleSetTransformer<TProperty, TNextProperty>
{
    public abstract string ErrorMessageTypeName { get; }

    public abstract bool TryTransform(TProperty property, out TNextProperty nextProperty);
}
public interface IRuleSetTransformerValidator<TNextProperty>
{
    ValidationResult Validate(TNextProperty instance);
    Task<ValidationResult> ValidateAsync(TNextProperty instance);
}
public interface IRuleSetTransformer<TProperty, TNextProperty, TRuleSetValidator> : IRuleSetTransformer<TProperty, TNextProperty> where TRuleSetValidator : IRuleSetValidator<TNextProperty>
{
    public TRuleSetValidator RuleSetValidator { get; }
}
public abstract class AbstractRuleSetTransformer<TProperty, TNextProperty, TRuleSetValidator> : AbstractRuleSetTransformer<TProperty, TNextProperty>, IRuleSetTransformer<TProperty, TNextProperty, TRuleSetValidator>, IRuleSetTransformerValidator<TNextProperty> where TRuleSetValidator : IRuleSetValidator<TNextProperty>
{
    protected AbstractRuleSetTransformer(TRuleSetValidator ruleSetValidator)
    {
        RuleSetValidator = ruleSetValidator;
    }

    public ValidationResult Validate(TNextProperty instance)
    {
        return RuleSetValidator.Validate(instance);
    }

    public async Task<ValidationResult> ValidateAsync(TNextProperty instance)
    {
        return await RuleSetValidator.ValidateAsync(instance);
    }

    public TRuleSetValidator RuleSetValidator { get; }
}