using FluentValidation.Validators;
using System.Reflection;

namespace Lexicom.Validation.Amenities;
public abstract class AbstractComparisonPropertyValidator<T, TProperty> : AbstractComparisonValidator<T, TProperty> where TProperty : IComparable<TProperty>, IComparable
{
    public AbstractComparisonPropertyValidator(TProperty value) : base(value)
    {
    }
    public AbstractComparisonPropertyValidator(Func<T, (bool HasValue, TProperty Value)> valueToCompareFunc, MemberInfo member, string memberDisplayName) : base(valueToCompareFunc, member, memberDisplayName)
    {
    }

    public abstract string DefaultMessageTemplate { get; }

    protected override string GetDefaultMessageTemplate(string? errorCode)
    {
        string? localizedMessageTemplate = Localized(errorCode, Name);

        if (!string.IsNullOrWhiteSpace(localizedMessageTemplate))
        {
            return localizedMessageTemplate;
        }

        return DefaultMessageTemplate ?? throw new NullReferenceException($"{nameof(DefaultMessageTemplate)} was null");
    }
}
