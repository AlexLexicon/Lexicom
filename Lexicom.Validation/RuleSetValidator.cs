using FluentValidation;
using FluentValidation.Results;
using Lexicom.Validation.Extensions;
using System.Collections.ObjectModel;

namespace Lexicom.Validation;
public interface IRuleSetValidator
{
    bool HasStandardizedErrorMessages { get; set; }
    bool HasSanitizedErrorMessages { get; set; }
    bool IsValid { get; }
    ObservableCollection<string> ValidationErrors { get; }

    void SetToValid();
}
public interface IRuleSetValidator<TProperty> : IRuleSetValidator, IValueValidator<TProperty>
{
    Func<TProperty, IEnumerable<string>> Validation { get; }
}
public interface IRuleSetValidator<TRuleSet, TProperty> : IRuleSetValidator<TProperty> where TRuleSet : IRuleSet<TProperty>
{
}
/// <exception cref="ArgumentNullException"/>
public class RuleSetValidator<TRuleSet, TProperty>(TRuleSet ruleSet) : BaseRuleSetValidator<TRuleSet, TProperty, TProperty>(ruleSet), IRuleSetValidator<TRuleSet, TProperty> where TRuleSet : IRuleSet<TProperty>
{
    protected override IEnumerable<string> ValidateAndGetErrorMessages(TProperty instance)
    {
        Validate(instance);

        var errors = new List<string>();
        foreach (string? error in ValidationErrors)
        {
            if (error is not null)
            {
                errors.Add(error);
            }
        }

        return errors;
    }
}
public interface IRuleSetValidator<TRuleSet, TProperty, TInProperty, TRuleSetTransformer> : IRuleSetValidator, IValueValidator<TProperty> where TRuleSet : IRuleSet<TProperty> where TRuleSetTransformer : IRuleSetTransfromer<TProperty, TInProperty>
{
    Func<TInProperty, IEnumerable<string>> Validation { get; }
}
/// <exception cref="ArgumentNullException"/>
public class RuleSetValidator<TRuleSet, TProperty, TInProperty, TRuleSetTransformer> : BaseRuleSetValidator<TRuleSet, TProperty, TInProperty>, IRuleSetValidator<TRuleSet, TProperty, TInProperty, TRuleSetTransformer> where TRuleSet : IRuleSet<TProperty> where TRuleSetTransformer : IRuleSetTransfromer<TProperty, TInProperty>
{
    /// <exception cref="ArgumentNullException"/>
    public RuleSetValidator(
        TRuleSet ruleSet, 
        TRuleSetTransformer ruleSetTransformer) : base(ruleSet)
    {
        ArgumentNullException.ThrowIfNull(ruleSetTransformer);

        Transformer = ruleSetTransformer;
    }

    public TRuleSetTransformer Transformer { get; }

    protected override IEnumerable<string> ValidateAndGetErrorMessages(TInProperty instance)
    {
        bool isValidated = true;
        if (Transformer is IRuleSetTransfromerValidator<TInProperty> transformerValidator)
        {
            ValidationResult result = transformerValidator.Validate(instance);

            isValidated = result.IsValid;

            if (!isValidated)
            {
                SetValidationErrors(result);
            }
        }

        if (isValidated)
        {
            if (Transformer.TryTransform(instance, out TProperty transformedInstance))
            {
                Validate(transformedInstance);
            }
            else
            {
                SetValidationErrors(new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure(propertyName: "", errorMessage: $"Must be a valid {Transformer.ErrorMessageTypeName}."),
            }));
            }
        }

        var errors = new List<string>();
        foreach (string? error in ValidationErrors)
        {
            if (error is not null)
            {
                errors.Add(error);
            }
        }

        return errors;
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
        ValidationErrors = [];
        Validation = ValidateAndGetErrorMessages;

        RuleFor(p => p.Value)
            .UseRuleSet(ruleSet);
    }

    public bool HasStandardizedErrorMessages { get; set; }
    public bool HasSanitizedErrorMessages { get; set; }
    public bool IsValid => !ValidationErrors.Any();
    public ObservableCollection<string> ValidationErrors { get; }
    public Func<TInProperty, IEnumerable<string>> Validation { get; }

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

    public virtual void SetToValid()
    {
        ValidationErrors.Clear();
    }

    protected abstract IEnumerable<string> ValidateAndGetErrorMessages(TInProperty instance);

    /// <exception cref="ArgumentNullException"/>
    protected virtual void SetValidationErrors(ValidationResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        IReadOnlyList<ValidationFailure> validationErrors = result.Errors;

        if (HasStandardizedErrorMessages)
        {
            validationErrors = validationErrors.StandardizeErrorMessages();
        }

        if (HasSanitizedErrorMessages)
        {
            validationErrors = validationErrors.SanitizeErrorMessages();
        }

        IReadOnlyList<string> errors = validationErrors.ToErrorMessages();

        ValidationErrors.Clear();
        foreach (string error in errors)
        {
            ValidationErrors.Add(error);
        }
    }
}