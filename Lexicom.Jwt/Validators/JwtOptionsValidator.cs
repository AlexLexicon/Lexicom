using FluentValidation;
using Lexicom.Jwt.Options;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;
using System.Text;

namespace Lexicom.Jwt.Validators;

public class JwtOptionsValidator : AbstractOptionsValidator<JwtOptions>
{
    //HS256 (HMAC-SHA256) requires a key of at least 256 bits, which is 32 bytes
    private const int MINIMUM_SYMMETRIC_SECURITY_KEY_BYTES = 32;

    /// <exception cref="ArgumentNullException"/>
    public JwtOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        ArgumentNullException.ThrowIfNull(requiredRuleSet);

        RuleFor(o => o.SymmetricSecurityKey)
            .UseRuleSet(requiredRuleSet)
            .Must(symmetricSecurityKey => symmetricSecurityKey is null || Encoding.UTF8.GetByteCount(symmetricSecurityKey) >= MINIMUM_SYMMETRIC_SECURITY_KEY_BYTES)
            .WithMessage($"'{{PropertyName}}' must be at least {MINIMUM_SYMMETRIC_SECURITY_KEY_BYTES} bytes (256 bits) when UTF-8 encoded for the HS256 signing algorithm.");
    }
}
