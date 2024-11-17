using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public class DirectoryExistsValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return Directory.Exists(value);
    }
}
public class DirectoryExistsPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(DirectoryExistsPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be an existing directory path.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return DirectoryExistsValidator.IsValid(value);
    }
}