using FluentValidation.Validators;
using System.Reflection;

namespace Lexicom.Validation.Amenities;

public abstract class AbstractComparisonPropertyValidator<T, TProperty> : AbstractComparisonValidator<T, TProperty>, IDefaultMessagePropertyValidator where TProperty : IComparable<TProperty>, IComparable
{
    public AbstractComparisonPropertyValidator(TProperty valueToCompare) : base(valueToCompare)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public AbstractComparisonPropertyValidator(Func<TProperty> valueToCompareFunc, MemberInfo? member = null, string? memberDisplayName = null) : this(_ => valueToCompareFunc.Invoke(), member, memberDisplayName)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public AbstractComparisonPropertyValidator(Func<T, TProperty> valueToCompareFunc, MemberInfo? member = null, string? memberDisplayName = null) : base(valueToCompareFunc, member, memberDisplayName)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }
    /// <exception cref="ArgumentNullException"/>
    public AbstractComparisonPropertyValidator(Func<T, (bool HasValue, TProperty Value)> valueToCompareFunc, MemberInfo? member = null, string? memberDisplayName = null) : base(valueToCompareFunc, member, memberDisplayName)
    {
        ArgumentNullException.ThrowIfNull(valueToCompareFunc);
    }

    public abstract string DefaultMessageTemplate { get; }

    protected override string GetDefaultMessageTemplate(string? errorCode)
    {
        string localizedMessageTemplate = Localized(errorCode, Name);

        return AbstractPropertyValidator.GetLocalizedOrDefaultMessageTemplate(this, localizedMessageTemplate);
    }
}
