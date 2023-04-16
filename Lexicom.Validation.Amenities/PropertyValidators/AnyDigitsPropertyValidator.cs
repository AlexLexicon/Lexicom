﻿using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public class AnyDigitsPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(AnyDigitsPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must contain any digit character.";

    public override string Name => NAME;
    public override string DefaultMessageTemplate => DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return value.Any(char.IsDigit);
    }
}
