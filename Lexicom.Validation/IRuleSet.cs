using FluentValidation;

namespace Lexicom.Validation;
public interface IRuleSet<TProperty>
{
    /// <exception cref="ArgumentNullException"/>
    void Use<T>(IRuleBuilderOptions<T, TProperty> ruleBuilder);
}