﻿using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public static class NotEscapedCharactersValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return !value.Any(Constants.CHARACTERS_ESCAPED.Contains);
    }
}
public class NotEscapedCharactersPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(NotEscapedCharactersPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must not contain any escaped characters.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return NotEscapedCharactersValidator.IsValid(value);
    }
}
