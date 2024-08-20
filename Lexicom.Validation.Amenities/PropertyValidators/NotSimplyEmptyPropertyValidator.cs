using FluentValidation;
using System.Collections;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class NotSimplyEmptyValidator<T>
{
    public static bool IsValid(T value)
    {
        switch (value)
        {
            case null:
                return true;
            case string s when s == string.Empty:
            case Guid g when g == Guid.Empty:
            case ICollection { Count: 0 }:
            case Array { Length: 0 }:
            case IEnumerable e when !e.GetEnumerator().MoveNext():
                return false;
        }

        return true;
    }
}
public class NotSimplyEmptyPropertyValidator<T, TProperty> : AbstractPropertyValidator<T, TProperty>
{
    public const string NAME = nameof(NotSimplyEmptyPropertyValidator<T, TProperty>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "The '{PropertyName}' field is required.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return NotSimplyEmptyValidator<TProperty>.IsValid(value);
    }
}