using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityOptionsSignInValidator : AbstractValidator<SignInOptions>
{
    public IdentityOptionsSignInValidator()
    {
        //RuleFor(o => o.RequireConfirmedEmail);

        //RuleFor(o => o.RequireConfirmedPhoneNumber);

        //RuleFor(o => o.RequireConfirmedAccount);
    }
}