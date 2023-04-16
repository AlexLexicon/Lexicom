using FluentValidation;
using Lexicom.Authority.Options;
using Lexicom.Validation.Options;

namespace Lexicom.Authority.Validators;
public class AuthorityOptionsValidator : AbstractOptionsValidator<AuthorityOptions>
{
    public AuthorityOptionsValidator()
    {
        RuleFor(o => o.AccessTokenValidTimeSpan)
            .GreaterThan(TimeSpan.Zero);

        RuleFor(o => o.RefreshTokenValidTimeSpan)
            .GreaterThan(TimeSpan.Zero);
    }
}
