using FluentValidation;
using FluentValidation.Results;
using Lexicom.Validation.Extensions;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

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
}
public interface IRuleSetValidator<TRuleSet, TProperty, TTransformer, TNextProperty> : IRuleSetValidator<TProperty>, IValueValidator<TProperty> where TRuleSet : IRuleSet<TProperty> where TTransformer : IRuleSetTransfromer<TProperty, TNextProperty>
{
    TTransformer Transformer { get; }
}
/// <exception cref="ArgumentNullException"/>
public class RuleSetValidator<TRuleSet, TProperty, TTransformer, TNextProperty> : BaseRuleSetValidator<TRuleSet, TProperty, TNextProperty>, IRuleSetValidator<TRuleSet, TProperty, TTransformer, TNextProperty> where TRuleSet : IRuleSet<TProperty> where TTransformer : IRuleSetTransfromer<TProperty, TNextProperty>
{
    /// <exception cref="ArgumentNullException"/>
    public RuleSetValidator(
        TRuleSet ruleSet, 
        TTransformer ruleSetTransformer) : base(ruleSet)
    {
        ArgumentNullException.ThrowIfNull(ruleSetTransformer);

        Transformer = ruleSetTransformer;
    }

    public TTransformer Transformer { get; }

    protected override ValidationResult PostValidate(TProperty instance, ValidationResult result)
    {
        if (result.IsValid)
        {
            if (!TryTransform(instance, out TNextProperty nextInstance, out ValidationResult? tryResult))
            {
                return tryResult;
            }

            if (Transformer is IRuleSetTransfromerValidator<TNextProperty> transformerValidator)
            {
                return transformerValidator.Validate(nextInstance);
            }
        }

        return result;
    }

    protected override async Task<ValidationResult> PostValidateAsync(TProperty instance, ValidationResult result)
    {
        if (result.IsValid)
        {
            if (!TryTransform(instance, out TNextProperty nextInstance, out ValidationResult? tryResult))
            {
                return tryResult;
            }

            if (Transformer is IRuleSetTransfromerValidator<TNextProperty> transformerValidator)
            {
                return await transformerValidator.ValidateAsync(nextInstance);
            }
        }

        return result;
    }

    protected virtual bool TryTransform(TProperty instance, out TNextProperty nextInstance, [NotNullWhen(false)] out ValidationResult? result)
    {
        result = null;

        bool isTransformable = Transformer.TryTransform(instance, out nextInstance);

        if (!isTransformable)
        {
            result = new ValidationResult(
            [
                new ValidationFailure(propertyName: "", errorMessage: $"Must be a valid {Transformer.ErrorMessageTypeName}."),
            ]);
        }

        return isTransformable;
    }
}
public abstract class BaseRuleSetValidator<TRuleSet, TProperty, TNextProperty> : AbstractValueValidator<TProperty> where TRuleSet : IRuleSet<TProperty>
{
    /// <exception cref="ArgumentNullException"/>
    public BaseRuleSetValidator(TRuleSet ruleSet)
    {
        ArgumentNullException.ThrowIfNull(ruleSet);

        HasStandardizedErrorMessages = true;
        HasSanitizedErrorMessages = true;
        ValidationErrors = [];
        Validation = ValidateForValidation;

        RuleFor(p => p.Value)
            .UseRuleSet(ruleSet);
    }

    public bool HasStandardizedErrorMessages { get; set; }
    public bool HasSanitizedErrorMessages { get; set; }
    public bool IsValid => !ValidationErrors.Any();
    public ObservableCollection<string> ValidationErrors { get; }
    public Func<TProperty, IEnumerable<string>> Validation { get; }

    /// <exception cref="ArgumentNullException"/>
    public override ValidationResult Validate(ValidationContext<ValidationValue<TProperty>> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        ValidationResult result = base.Validate(context);

        result = PostValidate(context.InstanceToValidate.Value, result);

        SetValidationErrors(result);

        return result;
    }

    /// <exception cref="ArgumentNullException"/>
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<ValidationValue<TProperty>> context, CancellationToken cancellation = default)
    {
        ArgumentNullException.ThrowIfNull(context);

        ValidationResult result = await base.ValidateAsync(context, cancellation);

        result = await PostValidateAsync(context.InstanceToValidate.Value, result);

        SetValidationErrors(result);

        return result;
    }

    public virtual void SetToValid()
    {
        ValidationErrors.Clear();
    }


    protected virtual ValidationResult PostValidate(TProperty instance, ValidationResult result)
    {
        return result;
    }

    protected virtual Task<ValidationResult> PostValidateAsync(TProperty instance, ValidationResult result)
    {
        return Task.FromResult(result);
    }

    protected virtual IEnumerable<string> ValidateForValidation(TProperty instance)
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