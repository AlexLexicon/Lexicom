﻿using Lexicom.Smtp.Extensions;
using Lexicom.Supports.ConsoleApp;

namespace Lexicom.Smtp.ConsoleApp.Extensions;
public static class ConsoleAppServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConsoleAppServiceBuilder AddSmtp(this IConsoleAppServiceBuilder builder, Action<ISmtpServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConsoleApplicationBuilder.Services.AddLexicomSmtp(builder.ConsoleApplicationBuilder.Configuration, configure);

        return builder;
    }
}
