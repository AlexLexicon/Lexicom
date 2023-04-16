using Lexicom.Extensions.IO;
using Lexicom.Smtp.Exceptions;
using Lexicom.Smtp.Options;
using Lexicom.Smtp.Validators;
using Microsoft.Extensions.Options;

namespace Lexicom.Smtp;
public class SmtpEmailFileClient : ISmtpEmailClient, ISmtpEmailHandler
{
    private readonly IOptions<SmtpEmailFileClientOptions> _smtpFileClientOptions;

    /// <exception cref="ArgumentNullException"/>
    public SmtpEmailFileClient(IOptions<SmtpEmailFileClientOptions> smtpFileClientOptions)
    {
        ArgumentNullException.ThrowIfNull(smtpFileClientOptions);

        _smtpFileClientOptions = smtpFileClientOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="OutputDirectoryNotValidException"/>
    public async Task SendEmailAsync(string toEmailAddress, string subject, string body)
    {
        ArgumentNullException.ThrowIfNull(toEmailAddress);
        ArgumentNullException.ThrowIfNull(subject);
        ArgumentNullException.ThrowIfNull(body);

        SmtpEmailFileClientOptions smtpFileClientConfiguration = _smtpFileClientOptions.Value;

        string? outputDirectoryPath = smtpFileClientConfiguration.OutputDirectoryPath;
        string? fileExtension = smtpFileClientConfiguration.FileExtension;
        string? fileName = smtpFileClientConfiguration.FileName;

        if (string.IsNullOrWhiteSpace(outputDirectoryPath) ||
            string.IsNullOrWhiteSpace(fileExtension) ||
            string.IsNullOrWhiteSpace(fileName))
        {
            throw SmtpEmailFileClientOptionsValidator.ToUnreachableException();
        }

        if (!Directory.Exists(outputDirectoryPath))
        {
            try
            {
                Directory.CreateDirectory(outputDirectoryPath);
            }
            catch (Exception e)
            {
                throw new OutputDirectoryNotValidException(smtpFileClientConfiguration.OutputDirectoryPath, e);
            }
        }

        if (!outputDirectoryPath.EndsWith('\\'))
        {
            outputDirectoryPath += '\\';
        }

        if (!fileExtension.StartsWith('.'))
        {
            fileExtension = '.' + fileExtension;
        }

        string fileNamePath = $"{outputDirectoryPath}{fileName}{fileExtension}";

        fileNamePath = fileNamePath.GetUniqueFileNamePath();

        string context = $"Sent to: '{toEmailAddress}'<br>Subject: '{subject}'<br>{body}";

        await File.WriteAllTextAsync(fileNamePath, context);
    }
}
