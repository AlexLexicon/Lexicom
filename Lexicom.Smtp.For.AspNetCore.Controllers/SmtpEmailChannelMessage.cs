namespace Lexicom.Smtp.AspNetCore.Controllers;
public class SmtpEmailChannelMessage
{
    /// <exception cref="ArgumentNullException"/>
    public SmtpEmailChannelMessage(
        string toEmailAddress,
        string subject,
        string body)
    {
        ArgumentNullException.ThrowIfNull(toEmailAddress);
        ArgumentNullException.ThrowIfNull(subject);
        ArgumentNullException.ThrowIfNull(body);

        ToEmailAddress = toEmailAddress;
        Subject = subject;
        Body = body;
    }

    public string ToEmailAddress { get; }
    public string Subject { get; }
    public string Body { get; }
}
