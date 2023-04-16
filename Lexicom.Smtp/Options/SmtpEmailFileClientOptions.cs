namespace Lexicom.Smtp.Options;
public class SmtpEmailFileClientOptions
{
    public string? OutputDirectoryPath { get; set; }
    public string? FileName { get; set; } = "email";
    public string? FileExtension { get; set; } = ".html";
}
