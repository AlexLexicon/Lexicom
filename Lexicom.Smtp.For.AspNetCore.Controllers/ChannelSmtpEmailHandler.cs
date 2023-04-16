using System.Threading.Channels;

namespace Lexicom.Smtp.AspNetCore.Controllers;
public class ChannelSmtpEmailHandler : ISmtpEmailHandler
{
    private readonly Channel<SmtpEmailChannelMessage> _channel;

    /// <exception cref="ArgumentNullException"/>
    public ChannelSmtpEmailHandler(Channel<SmtpEmailChannelMessage> channel)
    {
        ArgumentNullException.ThrowIfNull(channel);

        _channel = channel;
    }

    /// <exception cref="ArgumentNullException"/>
    public async Task SendEmailAsync(string toEmailAddress, string subject, string body)
    {
        ArgumentNullException.ThrowIfNull(toEmailAddress);
        ArgumentNullException.ThrowIfNull(subject);
        ArgumentNullException.ThrowIfNull(body);

        var message = new SmtpEmailChannelMessage(toEmailAddress, subject, body);

        await _channel.Writer.WriteAsync(message);   
    }
}
