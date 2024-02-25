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
    ReadOnlyObservableCollection<string> ValidationErrors { get; }

    void AddErrorAndInvalidate(string errorMessage);
    void SetValid();
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

        foreach (string? error in ValidationErrors)
        {
            if (error is not null)
            {
                yield return error;
            }
        }
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

        foreach (string? error in ValidationErrors)
        {
            if (error is not null)
            {
                yield return error;
            }
        }
    }
}
public abstract class BaseRuleSetValidator<TRuleSet, TProperty, TInProperty> : AbstractValueValidator<TProperty> where TRuleSet : IRuleSet<TProperty>
{
    private readonly ObservableCollection<string> _validationErrors;

    /// <exception cref="ArgumentNullException"/>
    public BaseRuleSetValidator(TRuleSet ruleSet)
    {
        ArgumentNullException.ThrowIfNull(ruleSet);

        _validationErrors = [];

        HasStandardizedErrorMessages = true;
        HasSanitizedErrorMessages = true;
        ValidationErrors = new ReadOnlyObservableCollection<string>(_validationErrors);
        Validation = ValidateAndGetErrorMessages;
        FailingErrors = [];

        RuleFor(p => p.Value)
            .UseRuleSet(ruleSet);
    }

    public bool HasStandardizedErrorMessages { get; set; }
    public bool HasSanitizedErrorMessages { get; set; }
    public bool IsValid => !ValidationErrors.Any();
    public ReadOnlyObservableCollection<string> ValidationErrors { get; }
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

    /// <exception cref="ArgumentNullException"/>
    public virtual void AddErrorAndInvalidate(string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(errorMessage);

        FailingErrors.Add(errorMessage);

        _validationErrors.Clear();
        foreach (string failingError in FailingErrors)
        {
            _validationErrors.Add(failingError);
        }
    }

    public virtual void SetValid()
    {
        FailingErrors.Clear();
        _validationErrors.Clear();
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

        _validationErrors.Clear();
        foreach (string error in errors)
        {
            _validationErrors.Add(error);
        }
    }
}