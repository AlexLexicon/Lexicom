using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity.Validators;
public class IdentityOptionsStoresValidator : AbstractValidator<StoreOptions>
{
    public IdentityOptionsStoresValidator()
    {
        RuleFor(o => o.MaxLengthForKeys)
            .GreaterThanOrEqualTo(0);

        //RuleFor(o => o.ProtectPersonalData);
    }
}