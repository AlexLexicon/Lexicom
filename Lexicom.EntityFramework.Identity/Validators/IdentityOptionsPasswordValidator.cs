using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityOptionsPasswordValidator : AbstractValidator<PasswordOptions>
{
    public IdentityOptionsPasswordValidator()
    {
        RuleFor(o => o.RequiredLength)
            .GreaterThan(0);

        RuleFor(o => o.RequiredUniqueChars)
            .GreaterThan(0);

        //RuleFor(o => o.RequireNonAlphanumeric);

        //RuleFor(o => o.RequireLowercase);

        //RuleFor(o => o.RequireUppercase);

        //RuleFor(o => o.RequireDigit);
    }
}