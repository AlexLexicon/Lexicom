using Lexicom.Smtp.Exceptions;
using Lexicom.Smtp.Options;
using Lexicom.Smtp.Validators;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;

namespace Lexicom.Smtp;
public class SmtpEmailMailClient : ISmtpEmailClient, ISmtpEmailHandler
{
    private readonly ILogger<SmtpEmailMailClient> _logger;
    private readonly IOptions<SmtpEmailMailClientOptions> _smtpEmailOptions;

    /// <exception cref="ArgumentNullException"/>
    public SmtpEmailMailClient(
        ILogger<SmtpEmailMailClient> logger,
        IOptions<SmtpEmailMailClientOptions> smtpEmailOptions)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(smtpEmailOptions);

        _logger = logger;
        _smtpEmailOptions = smtpEmailOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="EmailAddressNotValidException"/>
    /// <exception cref="SmtpNetworkCredentialsNotValidException"/>
    /// <exception cref="MailMessageNotValidException"/>
    /// <exception cref="EmailHostUnKnownException"/>
    /// <exception cref="EmailHostConnectionException"/>
    /// <exception cref="EmailHostNotSpecifiedException"/>
    public async Task SendEmailAsync(string toEmailAddress, string subject, string body)
    {
        ArgumentNullException.ThrowIfNull(toEmailAddress);
        ArgumentNullException.ThrowIfNull(subject);
        ArgumentNullException.ThrowIfNull(body);

        SmtpEmailMailClientOptions smtpEmailClientConfiguration = _smtpEmailOptions.Value;
        SmtpEmailMailClientOptionsValidator.ThrowIfNull(smtpEmailClientConfiguration.FromEmailAddress);
        SmtpEmailMailClientOptionsValidator.ThrowIfNull(smtpEmailClientConfiguration.Host);
        SmtpEmailMailClientOptionsValidator.ThrowIfNull(smtpEmailClientConfiguration.NetworkCredentialsUsername);
        SmtpEmailMailClientOptionsValidator.ThrowIfNull(smtpEmailClientConfiguration.NetworkCredentialsPassword);

        MailAddress fromMailAddress;
        try
        {
            fromMailAddress = new MailAddress(smtpEmailClientConfiguration.FromEmailAddress);
        }
        catch (Exception e) when (e is FormatException or ArgumentException)
        {
            throw new EmailAddressNotValidException(smtpEmailClientConfiguration.FromEmailAddress, e);
        }

        MailAddress toMailAddress;
        try
        {
            toMailAddress = new MailAddress(toEmailAddress);
        }
        catch (Exception e) when (e is FormatException or ArgumentException)
        {
            throw new EmailAddressNotValidException(toEmailAddress, e);
        }

        try
        {
            var mailMessage = new MailMessage(fromMailAddress, toMailAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body,
            };

            int? port = smtpEmailClientConfiguration.Port;

            SmtpClient smtpClient;
            if (port is not null)
            {
                _logger.LogInformation("Creating a new SmtpClient for the host '{host}:{port}'.", smtpEmailClientConfiguration.Host, port);

                smtpClient = new SmtpClient(smtpEmailClientConfiguration.Host, port.Value);
            }
            else
            {
                _logger.LogInformation("Creating a new SmtpClient for the host '{host}'.", smtpEmailClientConfiguration.Host);

                smtpClient = new SmtpClient(smtpEmailClientConfiguration.Host);
            }

            using (smtpClient)
            {
                smtpClient.EnableSsl = smtpEmailClientConfiguration.IsSslEnabled;

                if (string.IsNullOrWhiteSpace(smtpEmailClientConfiguration.NetworkCredentialsUsername) || 
                    string.IsNullOrWhiteSpace(smtpEmailClientConfiguration.NetworkCredentialsPassword))
                {
                    throw new SmtpNetworkCredentialsNotValidException();
                }

                smtpClient.Credentials = new NetworkCredential(smtpEmailClientConfiguration.NetworkCredentialsUsername, smtpEmailClientConfiguration.NetworkCredentialsPassword);

                _logger.LogInformation("SmtpClient.SendMailAsync initiated.");

                await smtpClient.SendMailAsync(mailMessage);

                _logger.LogInformation("SmtpClient.SendMailAsync succeeded.");
            }
        }
        catch (Exception e) when (e is FormatException or ArgumentNullException)
        {
            throw new MailMessageNotValidException(e);
        }
        catch (SmtpException e)
        {
            if (e.InnerException is SocketException socketException)
            {
                if (socketException.Message == "No such host is known.")
                {
                    throw new EmailHostUnKnownException(e);
                }
                else if (socketException.Message == "A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.")
                {
                    throw new EmailHostConnectionException(e);
                }
            }
            else if (e.InnerException is InvalidOperationException invalidOperationException)
            {
                if (invalidOperationException.Message == "The SMTP host was not specified.")
                {
                    throw new EmailHostNotSpecifiedException(e);
                }
            }

            throw;
        }
    }
}
