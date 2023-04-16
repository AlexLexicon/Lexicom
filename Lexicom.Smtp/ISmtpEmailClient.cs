namespace Lexicom.Smtp;
public interface ISmtpEmailClient
{
    /// <exception cref="ArgumentNullException"/>
    Task SendEmailAsync(string toEmailAddress, string subject, string body);
}