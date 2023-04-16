namespace Lexicom.Smtp;
public interface ISmtpEmailHandler
{
    /// <exception cref="ArgumentNullException"/>
    Task SendEmailAsync(string toEmailAddress, string subject, string body);
}
