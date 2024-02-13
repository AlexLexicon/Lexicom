using FluentValidation;
using FluentValidation.Results;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation;
public interface IRuleSetValidator
{
    bool HasStandardizedErrorMessages { get; set; }
    bool HasSanitizedErrorMessages { get; set; }
    bool IsValid { get; }
    IReadOnlyList<string> ValidationErrors { get; }

    void SetInvalid(string errorMessage);
    void SetValid();
}
public interface IRuleSetValidator<TRuleSet, TProperty> : IRuleSetValidator, IValueValidator<TProperty> where TRuleSet : IRuleSet<TProperty>
{
    Func<TProperty, IEnumerable<string>> Validation { get; }
}
/// <exception cref="ArgumentNullException"/>
public class RuleSetValidator<TRuleSet, TProperty>(TRuleSet ruleSet) : BaseRuleSetValidator<TRuleSet, TProperty, TProperty>(ruleSet), IRuleSetValidator<TRuleSet, TProperty> where TRuleSet : IRuleSet<TProperty>
{
    protected override IEnumerable<string> ValidateAndGetErrorMessages(TProperty instance)
    {
        Validate(instance);

        return ValidationErrors;
    }
}
public interface IRuleSetValidator<TRuleSet, TProperty, TInProperty, TRuleSetTransformer> : IRuleSetValidator, IValueValidator<TProperty> where TRuleSet : IRuleSet<TProperty> where TRuleSetTransformer : IRuleSetTransfromer<TProperty, TInProperty>, new()
{
    Func<TInProperty, IEnumerable<string>> Validation { get; }
}
/// <exception cref="ArgumentNullException"/>
public class RuleSetValidator<TRuleSet, TProperty, TInProperty, TRuleSetTransformer>(TRuleSet ruleSet) : BaseRuleSetValidator<TRuleSet, TProperty, TInProperty>(ruleSet), IRuleSetValidator<TRuleSet, TProperty, TInProperty, TRuleSetTransformer> where TRuleSet : IRuleSet<TProperty> where TRuleSetTransformer : IRuleSetTransfromer<TProperty, TInProperty>, new()
{
    protected override IEnumerable<string> ValidateAndGetErrorMessages(TInProperty instance)
    {
        var ruleSetTransformer = new TRuleSetTransformer();
        if (ruleSetTransformer.TryTransform(instance, out TProperty transformedInstance))
        {
            Validate(transformedInstance);
        }
        else
        {
            SetValidationErrors(new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure(propertyName: "", errorMessage: $"Must be a valid {ruleSetTransformer.ErrorMessageTypeName}."),
            }));
        }

        return ValidationErrors;
    }
}
public abstract class BaseRuleSetValidator<TRuleSet, TProperty, TInProperty> : AbstractValueValidator<TProperty> where TRuleSet : IRuleSet<TProperty>
{
    /// <exception cref="ArgumentNullException"/>
    public BaseRuleSetValidator(TRuleSet ruleSet)
    {
        ArgumentNullException.ThrowIfNull(ruleSet);

        HasStandardizedErrorMessages = true;
        HasSanitizedErrorMessages = true;
        ValidationErrors = new List<string>();
        Validation = ValidateAndGetErrorMessages;
        FailingErrors = [];

        RuleFor(p => p.Value)
            .UseRuleSet(ruleSet);
    }

    public bool HasStandardizedErrorMessages { get; set; }
    public bool HasSanitizedErrorMessages { get; set; }
    public bool IsValid => !ValidationErrors.Any();
    public IReadOnlyList<string> ValidationErrors { get; protected set; }
    public Func<TInProperty, IEnumerable<string>> Validation { get; }

    private List<string> FailingErrors { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public override ValidationResult Validate(ValidationContext<ValidationValue<TProperty>> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        ValidationResult result = base.Validate(context);

        SetValidationErrors(result);

        return result;
    }

    /// <exception cref="ArgumentNullException"/>
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<ValidationValue<TProperty>> context, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(context);

        ValidationResult result = await base.ValidateAsync(context, cancellation);

        SetValidationErrors(result);

        return result;
    }

    public virtual void SetInvalid(string errorMessage)
    {
        FailingErrors.Add(errorMessage);

        ValidationErrors = FailingErrors;
    }

    public virtual void SetValid()
    {
        FailingErrors.Clear();

        ValidationErrors = FailingErrors;
    }

    protected abstract IEnumerable<string> ValidateAndGetErrorMessages(TInProperty instance);

    /// <exception cref="ArgumentNullException"/>
    protected virtual void SetValidationErrors(ValidationResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        IReadOnlyList<ValidationFailure> initalValidationErrors = result.Errors;

        if (HasStandardizedErrorMessages)
        {
            initalValidationErrors = initalValidationErrors.StandardizeErrorMessages();
        }
        
        if (HasSanitizedErrorMessages)
        {
            initalValidationErrors = initalValidationErrors.SanitizeErrorMessages();
        }

        ValidationErrors = initalValidationErrors.ToErrorMessages();
    }
}