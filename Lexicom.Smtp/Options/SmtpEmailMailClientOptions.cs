namespace Lexicom.Smtp.Options;

public class SmtpEmailMailClientOptions
{
    public string? FromEmailAddress { get; set; }
    public string? Host { get; set; }
    public int? Port { get; set; }
    //ssl is enabled by default so that credentials are never sent in plaintext unless explicitly opted into
    public bool IsSslEnabled { get; set; } = true;
    public string? NetworkCredentialsUsername { get; set; }
    public string? NetworkCredentialsPassword { get; set; }
}
