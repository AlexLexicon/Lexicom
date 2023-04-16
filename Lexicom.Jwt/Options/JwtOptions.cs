namespace Lexicom.Jwt.Options;
public class JwtOptions
{
    public const string ACCESS_TOKEN_SECTION = "AccessTokenOptions";
    public const string REFRESH_TOKEN_SECTION = "RefreshTokenOptions";

    public string? SymmetricSecurityKey { get; set; }
}
