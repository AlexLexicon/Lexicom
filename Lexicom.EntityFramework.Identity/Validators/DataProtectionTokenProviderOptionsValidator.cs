using FluentValidation;
using Lexicom.Validation.Options;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Validators;
public class DataProtectionTokenProviderOptionsValidator : AbstractOptionsValidator<DataProtectionTokenProviderOptions>
{
    public DataProtectionTokenProviderOptionsValidator()
    {
        RuleFor(o => o.TokenLifespan)
            .GreaterThan(TimeSpan.Zero);
    }
}
