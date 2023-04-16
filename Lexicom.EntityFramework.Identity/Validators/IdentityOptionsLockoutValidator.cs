using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityOptionsLockoutValidator : AbstractValidator<LockoutOptions>
{
    public IdentityOptionsLockoutValidator()
    {
        //RuleFor(o => o.AllowedForNewUsers);

        RuleFor(o => o.MaxFailedAccessAttempts)
            .GreaterThan(0);

        RuleFor(o => o.DefaultLockoutTimeSpan)
            .GreaterThan(TimeSpan.Zero);
    }
}