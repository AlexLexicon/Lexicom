namespace Lexicom.Validation.Amenities.Options;
public class PasswordRequirementsRuleSetOptions
{
    public int? MinimumLength { get; set; }
    public int? MaximumLength { get; set; }
    public bool? RequireDigit { get; set; }
    public bool? RequireNonAlphanumeric { get; set; }
    public bool? RequireLowercase { get; set; }
    public bool? RequireUppercase { get; set; }
    public int? RequiredUniqueChars { get; set; } = 1;
}
