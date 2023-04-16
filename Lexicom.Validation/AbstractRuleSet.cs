using FluentValidation;

namespace Lexicom.Validation;
public abstract class AbstractRuleSet<TProperty> : IRuleSet<TProperty>
{
    /// <exception cref="ArgumentNullException"/>
    public abstract void Use<T>(IRuleBuilderOptions<T, TProperty> ruleBuilder);
}