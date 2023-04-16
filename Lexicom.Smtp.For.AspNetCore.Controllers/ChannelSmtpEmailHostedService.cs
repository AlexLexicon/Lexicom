using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace Lexicom.Smtp.AspNetCore.Controllers;
public class ChannelSmtpEmailHostedService : BackgroundService
{
    private readonly ILogger<ChannelSmtpEmailHostedService> _logger;
    private readonly Channel<SmtpEmailChannelMessage> _channel;
    private readonly ISmtpEmailClient _smtpEmailClient;

    /// <exception cref="ArgumentNullException"/>
    public ChannelSmtpEmailHostedService(
        ILogger<ChannelSmtpEmailHostedService> logger,
        Channel<SmtpEmailChannelMessage> channel,
        ISmtpEmailClient smtpEmailClient)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(channel);
        ArgumentNullException.ThrowIfNull(smtpEmailClient);

        _logger = logger;
        _channel = channel;
        _smtpEmailClient = smtpEmailClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        ChannelReader<SmtpEmailChannelMessage> channelReader = _channel.Reader;

        while (await channelReader.WaitToReadAsync(stoppingToken))
        {
            try
            {
                SmtpEmailChannelMessage message = await channelReader.ReadAsync(stoppingToken);

                await _smtpEmailClient.SendEmailAsync(message.ToEmailAddress, message.Subject, message.Body);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "The background hosted service '{hostedServiceName}' encountered an unexpected error.", nameof(ChannelSmtpEmailHostedService));
            }
        }
    }
}
