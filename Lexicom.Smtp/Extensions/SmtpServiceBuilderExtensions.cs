using Lexicom.DependencyInjection.Extensions;
using Lexicom.Smtp.Options;
using Lexicom.Smtp.Validators;
using Lexicom.Validation.Options.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Smtp.Extensions;
public static class SmtpServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ISmtpServiceBuilder AddMailClient(this ISmtpServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services
            .AddOptions<SmtpEmailMailClientOptions>()
            .BindConfiguration()
            .Validate<SmtpEmailMailClientOptions, SmtpEmailMailClientOptionsValidator>()
            .ValidateOnStart();

        builder.Services.TryAddSingleton<SmtpEmailMailClient>();

        builder.Services.TryAddSingleton<ISmtpEmailClient>(sp =>
        {
            return sp.GetRequiredService<SmtpEmailMailClient>();
        });

        builder.Services.AddSingleton<ISmtpEmailHandler>(sp =>
        {
            return sp.GetRequiredService<SmtpEmailMailClient>();
        });

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static ISmtpServiceBuilder AddFileClient(this ISmtpServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services
            .AddOptions<SmtpEmailFileClientOptions>()
            .BindConfiguration()
            .Validate<SmtpEmailFileClientOptions, SmtpEmailFileClientOptionsValidator>()
            .ValidateOnStart();

        builder.Services.TryAddSingleton<SmtpEmailFileClient>();

        builder.Services.TryAddSingleton<ISmtpEmailClient>(sp =>
        {
            return sp.GetRequiredService<SmtpEmailFileClient>();
        });

        builder.Services.AddSingleton<ISmtpEmailHandler>(sp =>
        {
            return sp.GetRequiredService<SmtpEmailFileClient>();
        });

        return builder;
    }
}
