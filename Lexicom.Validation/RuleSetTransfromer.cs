using FluentValidation.Results;

namespace Lexicom.Validation;
public interface IRuleSetTransfromer<TProperty, TInProperty>
{
    string ErrorMessageTypeName { get; }

    bool TryTransform(TInProperty inProperty, out TProperty property);
}
public abstract class AbstractRuleSetTransformer<TProperty, TInProperty> : IRuleSetTransfromer<TProperty, TInProperty>
{
    public abstract string ErrorMessageTypeName { get; }

    public abstract bool TryTransform(TInProperty inProperty, out TProperty property);
}
public interface IRuleSetTransfromerValidator<TInProperty>
{
    ValidationResult Validate(TInProperty instance);
}
public interface IRuleSetTransfromer<TProperty, TInProperty, TRuleSetValidator> : IRuleSetTransfromer<TProperty, TInProperty> where TRuleSetValidator : IRuleSetValidator<TInProperty>
{
    public TRuleSetValidator RuleSetValidator { get; }
}
public abstract class AbstractRuleSetTransformer<TProperty, TInProperty, TRuleSetValidator> : AbstractRuleSetTransformer<TProperty, TInProperty>, IRuleSetTransfromer<TProperty, TInProperty, TRuleSetValidator>, IRuleSetTransfromerValidator<TInProperty> where TRuleSetValidator : IRuleSetValidator<TInProperty>
{
    protected AbstractRuleSetTransformer(TRuleSetValidator ruleSetValidator)
    {
        RuleSetValidator = ruleSetValidator;
    }

    public ValidationResult Validate(TInProperty instance)
    {
        return RuleSetValidator.Validate(instance);
    }

    public TRuleSetValidator RuleSetValidator { get; }
}