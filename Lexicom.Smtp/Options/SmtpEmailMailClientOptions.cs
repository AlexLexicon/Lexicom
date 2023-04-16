namespace Lexicom.Smtp.Options;
public class SmtpEmailMailClientOptions
{
    public string? FromEmailAddress { get; set; }
    public string? Host { get; set; }
    public int? Port { get; set; }
    public bool IsSslEnabled { get; set; }
    public string? NetworkCredentialsUsername { get; set; }
    public string? NetworkCredentialsPassword { get; set; }
}
